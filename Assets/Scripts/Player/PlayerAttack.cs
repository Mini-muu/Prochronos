using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private bool doDamage = false;
    private bool bladeDown = false;
    private float attackDamage;
    private float mainAttackDamage = 1;
    private bool mainAttack = false;
    private float superAttackDamage = 3;
    private float superAttackTime = 0;
    private bool superAttack = false;
    private bool superAttackCharging = false;
    private bool parry = false;
    private static bool canParry = false;
    [SerializeField, Tooltip("Knockback of the main attack")] private float mainKnockback = 0.66f; //Rinculo
    [SerializeField, Tooltip("Knockback of the secondary attack")] private float superKnockback = 1; //Rinculo dell'attacco più potente
    private bool canAttack = true;
    private float newPositionX;
    private Health health;
    private Animator animator;
    private PlayerMovement playerMovement;
    private EnemyHealth enemyHealth;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask enemyLayer;
    [Header("Attack box parameters:")] //Header: permette di creare una raccolta di richieste presenti nel suddetto componente una volta presente nell'inspector, aiutando, quindi, ad organizzare meglio le richieste di tipo [SerializeField] quando vengono visualizzate.
    [SerializeField, Tooltip("Distance of the 'attack' box from the BoxCollider2D")] private float attackRange;
    [SerializeField, Tooltip("Width of the 'attack' box")] private float attackAreaWidth;

    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        animator = gameObject.GetComponent<Animator>();
        newPositionX = transform.position.x;
        health = gameObject.GetComponent<Health>();
    }

    void Update()
    {
        if (!pauseMenu.getIsOpen())
        {
            fightInteraction();
            animator.SetBool("mainAttack", mainAttack);
            animator.SetBool("superAttack", superAttack);
            animator.SetBool("parry", parry);
        }
    }

    //Importante: notare bene che il metodo sottostante è sempre attivo nell'editor del gioco e non solo durante la simulazione.
    private void OnDrawGizmos()
    {
        float range = (attackAreaWidth / 2f) + attackRange;
        float attackCubeCenter = boxCollider.bounds.center.x + transform.localScale.x * range;

        //Disegno l'area d'attacco della spada.
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(attackCubeCenter, boxCollider.bounds.center.y, boxCollider.bounds.center.z),
            new Vector3(attackAreaWidth, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    public void onFightActions(InputAction.CallbackContext v)
    {
        switch (v.action.name) {
            case "MainAttack":
                if (v.action.triggered != false) attack(ref mainAttack, mainAttackDamage, getMainKnockback());
                break;
            case "Parry":
                if (canParry)
                {
                    parry = true;
                }
                break;
            case "SuperAttack":
                if (v.started || v.action.triggered) {
                    superAttackCharging = true; Debug.Log("Press the button for at least 2 seconds");
                } else superAttackCharging = false;
                break;
        }
    }

    private void fightInteraction()
    {
        if (superAttackCharging && superAttackTime < 6)
        {
            superAttackTime += Time.deltaTime;
            Debug.Log("Pressing right mouse button: " + superAttackTime);
        }
        else if ((!superAttackCharging && superAttackTime > 2) || superAttackTime > 6)
        {
            attack(ref superAttack, superAttackDamage, getSuperAttackDamage());
        }
        else
        {
            superAttackTime = 0;
        }

        if (health.getHurt())
        {
            parry = false;
            attackFinished();
        }

        if (bladeDown) //Nel caso in cui il personaggio abbia affondato la spada ricevi il rinculo
        {
            if (mainAttack)
            {
                playerMovement.knockbackDirection(mainKnockback, newPositionX);
            } else if (superAttack)
            {
                playerMovement.knockbackDirection(superKnockback, newPositionX);
            }
        }

        if (doDamage)
        {
            float range = (attackAreaWidth / 2f) + attackRange;
            float attackCubeCenter = boxCollider.bounds.center.x + transform.localScale.x * range;
            RaycastHit2D hit = Physics2D.BoxCast(new Vector3(attackCubeCenter, boxCollider.bounds.center.y, boxCollider.bounds.center.z),
                new Vector3(attackAreaWidth, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
                0, Vector2.right, 0, enemyLayer);

            if (hit.collider != null && doDamage)
            {
                enemyHealth = hit.transform.GetComponent<EnemyHealth>();
                enemyHealth.reduceHealth(attackDamage);
                Debug.Log("Attack damage: " + attackDamage);
                doDamage = false;
            } else
            {
                doDamage = false;
            }
        }
    }

    private void attack(ref bool attackType, float damage, float knockBack)
    {
        canAttack = false;
        attackType = true;
        attackDamage = damage;
        if (playerMovement.isFacingRight)
        {
            newPositionX = transform.position.x - knockBack;
        }
        else
        {
            newPositionX = transform.position.x + knockBack;
        }
    }

    public void attackFinished() { //Usato in animazione
        canAttack = true;
        mainAttack = false;
        superAttack = false;
        bladeDown = false;
        superAttackTime = 0;
    }

    private void bladeDownKnockback() //Usato in animazione
    {
        bladeDown = true;
    }

    private void doDamageMethod() //Usato in animazione
    {
        doDamage = true;
    }

    private void parryFinished() //Usato in animazione
    {
        parry = false;
    }

    //Metodi setter
    public void setMainAttack(bool mainAttack) { this.mainAttack = mainAttack; }

    //Metodi getter
    public bool getMainAttack() { return mainAttack; }
    public float getmainAttackDamage() { return mainAttackDamage; }
    public float getMainKnockback() { return mainKnockback; }
    public float getSuperKnockback() { return superKnockback; }
    public float getSuperAttackDamage() { return superAttackDamage; }
    public bool getCanAttack() { return canAttack; }
    public bool getParry() { return parry; }
    public static void setCanParry(bool parry) { canParry = parry; }

    //Metodi reducer
    public void reduceMainAttackDamage(float reduceMainAttackDamage)
    {
        mainAttackDamage -= reduceMainAttackDamage;
    }
    public void reduceMainKnockback(float reduceKnockback)
    {
        mainKnockback -= reduceKnockback;
    }
    public void reduceSuperAttackDamage(float reduceSuperAttackDamage)
    {
        mainAttackDamage -= reduceSuperAttackDamage;
    }
}
