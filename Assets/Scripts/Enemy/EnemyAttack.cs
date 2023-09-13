using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField, Tooltip("Set how much the enemy has to wait to repeat the attack")] private float attackCoolDown = 1;
    private float damage = 1;
    private float coolDownTimer = Mathf.Infinity;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField, Tooltip("Distance of the 'attack' box from the BoxCollider2D (his body)")] private float attackRange = 0.01f;
    [SerializeField, Tooltip("Distance of the enemy view box from the BoxCollider2D (his body)")] private float viewRangeBack = 0.55f;
    [SerializeField, Tooltip("Width of the enemy view box")] private float viewAreaWidth;
    [SerializeField, Tooltip("Width of the 'attack' box")] private float attackAreaWidth;
    [SerializeField] private Transform enemy;
    private Animator animator;
    private Health playerHealth;
    private bool inAttack = false;
    [SerializeField] private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private EnemyMovement enemyMovement;

    void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<Transform>();
        enemyMovement = GetComponentInParent<EnemyMovement>();
    }

    void Update()
    {
        if (inAttack == false)
        {
            coolDownTimer += Time.deltaTime;
            PlayerAttack.setCanParry(false);
        } else
        {
            PlayerAttack.setCanParry(true);
        }
        if (canAttack()) {
            if (coolDownTimer >= attackCoolDown)
            {
                coolDownTimer = 0;
                setInAttack(true);
            }
        }
        enemyView();
        animator.SetBool("mainAttack", inAttack);
    }

    //Importante: notare bene che il metodo sottostante è sempre attivo nell'editor del gioco e non solo durante la simulazione.
    private void OnDrawGizmos()
    {
        float range = boxCollider.size.x + attackRange;
        float attackCubeCenterFrontX = boxCollider.bounds.center.x + transform.localScale.x * range;
        float checkCubeCenterBackX = boxCollider.bounds.center.x + -transform.localScale.x * (boxCollider.bounds.size.x + viewRangeBack);
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(new Vector3(attackCubeCenterFrontX, boxCollider.bounds.center.y, boxCollider.bounds.center.z),
            new Vector3(attackAreaWidth, boxCollider.bounds.size.y, boxCollider.bounds.size.z)); //Si fa uso di transform.localScale.x perché é una delle coordinate che stabiliscono la grandezza del personaggio nello spazio

        Gizmos.color = Color.blue;

        Gizmos.DrawWireCube(new Vector3(attackCubeCenterFrontX + transform.localScale.x * (viewAreaWidth / 2 + attackAreaWidth / 2 + attackRange), boxCollider.bounds.center.y, boxCollider.bounds.center.z),
            new Vector3(viewAreaWidth, boxCollider.bounds.size.y, boxCollider.bounds.size.z));

        Gizmos.DrawWireCube(new Vector3(checkCubeCenterBackX, boxCollider.bounds.center.y, boxCollider.bounds.center.z),
            new Vector3(viewAreaWidth / 2, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private bool canAttack() {
        float range = (attackAreaWidth / 2f) + attackRange;
        float attackCubeCenter = boxCollider.bounds.center.x + enemy.localScale.x * range;
        RaycastHit2D hit = Physics2D.BoxCast(new Vector3(attackCubeCenter, boxCollider.bounds.center.y, boxCollider.bounds.center.z),
            new Vector3(attackAreaWidth, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.right, 0, playerLayer);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player") {
                playerMovement = hit.transform.GetComponent<PlayerMovement>();
                playerHealth = hit.transform.GetComponent<Health>();
                playerAttack = hit.transform.GetComponent<PlayerAttack>();
            }
        }

        return hit.collider != null;

    }

    private void enemyView()
    {
        float range = boxCollider.size.x + attackRange;
        float attackCubeCenterFrontX = boxCollider.bounds.center.x + transform.localScale.x * range;
        float checkCubeCenterBackX = boxCollider.bounds.center.x + -transform.localScale.x * (boxCollider.bounds.size.x + viewRangeBack);

        RaycastHit2D hitFront = Physics2D.BoxCast(new Vector3(attackCubeCenterFrontX + transform.localScale.x * (viewAreaWidth / 2 + attackAreaWidth / 2 + attackRange), boxCollider.bounds.center.y, boxCollider.bounds.center.z),
            new Vector3(viewAreaWidth, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.right, 0, playerLayer);

        RaycastHit2D hitBack = Physics2D.BoxCast(new Vector3(checkCubeCenterBackX, boxCollider.bounds.center.y, boxCollider.bounds.center.z),
            new Vector3(viewAreaWidth / 2, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.right, 0, playerLayer);

        if (canAttack() == false)
        {
            if (hitFront.collider != null)
            {
                if (hitFront.collider.tag == "Player")
                {
                    if (!playerMovement.getRollPressed())
                    {
                        enemyMovement.followItem(hitFront.collider.transform.position.x);
                    }
                }
            }
            else if (hitBack.collider != null)
            {
                if (hitBack.collider.tag == "Player")
                {
                    if (!playerMovement.getRollPressed())
                    {
                        enemyMovement.followItem(hitBack.collider.transform.position.x);
                    }
                }
            }
            else
            {
                enemyMovement.walk();
            }
        }
    }

    private void attack() { //Usato in animazione
        if (canAttack())
        {
            if (!playerMovement.getRollPressed() && !playerAttack.getParry())
            {
                playerHealth.reduceHealth(damage);
            }
        }
    }

    private void attackFinished() //Usato in animazione
    {
        setInAttack(false);
    }

    public void setInAttack(bool change) {
        inAttack = change;
    }

    public bool getInAttack()
    {
        return inAttack;
    }
}
