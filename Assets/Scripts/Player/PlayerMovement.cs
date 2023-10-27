using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 pos_iniziale;

    private float horizontal;
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float runSpeed = 8f;
    private float speed;
    [SerializeField] public bool isFacingRight = true;
    private float feetRectHeight = 0.1f;
    private int nJumps = 0;
    private float previousDirection = 0;
    private bool rollPressed = false;
    private float startBoxColliderSizeX;
    private float startBoxColliderSizeY;
    private float startBoxColliderOffsetX;
    private float startBoxColliderOffsetY;
    private LayerMask startBoxColliderLayerMask;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField, Tooltip("This parameter allows you to set the power of the player's jump. So it can be used with the gravity scale to change the speed " +
        "of the jump")] private float jumpingPower = 6f;
    [SerializeField] private BoxCollider2D collisionWithoutEnemy;

    //Variabili che permettono di modificare lo spazio che occupa il BoxCollider. Da usare durante la rotolata (roll).
    [Header("BoxCollider changes during roll animation:")] //Header: permette di creare una raccolta di richieste presenti nel suddetto componente una volta presente nell'inspector, aiutando, quindi, ad organizzare meglio le richieste di tipo [SerializeField] quando vengono visualizzate.
    [SerializeField] private float changeBoxColliderSizeInrollX = 0.2f;
    [SerializeField] private float changeBoxColliderSizeInrollY = 0.1f;
    [SerializeField] private float changeBoxColliderOffsetInrollX = -0.15f;
    [SerializeField] private float changeBoxColliderOffsetInrollY = -0.14f;

    private Rigidbody2D player;
    private Animator animator;
    private PlayerAttack playerAttack;
    private Health health;
    private bool runPressed = false;
    //private PlayerInput playerInput;

    void Start()
    {
        
        player = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        playerAttack = gameObject.GetComponent<PlayerAttack>();
        health = GetComponent<Health>();
        pos_iniziale = transform.position;
        speed = walkSpeed;
        startBoxColliderSizeX = boxCollider.size.x;
        startBoxColliderSizeY = boxCollider.size.y;
        startBoxColliderOffsetX = boxCollider.offset.x;
        startBoxColliderOffsetY = boxCollider.offset.y;
        startBoxColliderLayerMask = boxCollider.forceSendLayers;
        //playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (!pauseMenu.getIsOpen())
        {
            // Resetta la posizione del PG se cade dallo schermo
            if (transform.position.y < -5.6f) transform.position = pos_iniziale;
            movement();
            Flip();
        } else
        {
            horizontal = 0;
        }
        animator.SetFloat("Speed", Mathf.Abs(horizontal * speed));
        animator.SetBool("falling", !checkBottomTerrain() && nJumps == 0);
        animator.SetInteger("nJump", nJumps);
        animator.SetBool("strike", rollPressed);
    }

    private void FixedUpdate()
    {
        if (playerAttack.getCanAttack() && !checkBottomEnemy() && health.getHealth() > 0) { player.velocity = new Vector2(horizontal * speed, player.velocity.y); }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision: "+(LayerMask.GetMask(LayerMask.LayerToName(collision.gameObject.layer)) == groundLayer.value)+", terrain: "+checkBottomTerrain());
        if (LayerMask.GetMask(LayerMask.LayerToName(collision.gameObject.layer)) == groundLayer.value && checkBottomTerrain())
        {
            if (nJumps > 0 && playerAttack.getMainAttack())
            {
                playerAttack.attackFinished();
            }
            nJumps = 0;
        } else if (LayerMask.GetMask(LayerMask.LayerToName(collision.gameObject.layer)) == enemyLayer.value && rollPressed) {
            boxCollider.forceSendLayers = collisionWithoutEnemy.forceSendLayers;
            boxCollider.forceReceiveLayers = collisionWithoutEnemy.forceReceiveLayers;
        }
    }

    private void OnDrawGizmos() //Il rettangolo disegnato di giallo ai piedi del giocatore potrebbe essere usato in seguito per verificare il terreno e modificare così il contatore del salto solo quando il terreno si trova a contatto con i piedi del giocatore
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(boxCollider.bounds.center.x, -1 * (boxCollider.bounds.size.y/2) + boxCollider.bounds.center.y - (feetRectHeight / 2), boxCollider.bounds.center.z),
            new Vector3(boxCollider.bounds.size.x, feetRectHeight, boxCollider.bounds.size.z));
    }

    private bool checkBottomTerrain()
    {
        RaycastHit2D hit = Physics2D.BoxCast(new Vector3(boxCollider.bounds.center.x, -1 * (boxCollider.bounds.size.y / 2) + boxCollider.bounds.center.y - (feetRectHeight / 2), boxCollider.bounds.center.z),
                new Vector3(boxCollider.bounds.size.x, feetRectHeight, boxCollider.bounds.size.z),
                    0, Vector2.right, 0, groundLayer);
        if (hit.collider != null)
        {
            return true;
        } else
        {
            return false;
        }
    }

    private bool checkBottomEnemy()
    {
        if (!rollPressed)
        {
            RaycastHit2D hit = Physics2D.BoxCast(new Vector3(boxCollider.bounds.center.x, -1 * (boxCollider.bounds.size.y / 2) + boxCollider.bounds.center.y - (feetRectHeight / 2), boxCollider.bounds.center.z),
                new Vector3(boxCollider.bounds.size.x, feetRectHeight, boxCollider.bounds.size.z),
                    0, Vector2.right, 0, enemyLayer);
            if (hit.collider != null)
            {
                if (previousDirection == 0 && nJumps > 0)
                {
                    previousDirection = transform.localScale.x;
                    player.velocity = new Vector2(previousDirection * 10, player.velocity.y);
                }
                player.velocity = new Vector2(previousDirection * 10, player.velocity.y);
                return true;
            }
            else
            {
                previousDirection = 0;
            }
        }
        return false;
    }

    private void Flip() //Codice per cambiare la direzione dell'immagine del personaggio
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void movement() {
        runChecker();
        rollChecker();
    }

    public void onMovement(InputAction.CallbackContext value) {  //Da verificare l'efficenza nel caso dell'interazzione con più azioni nello stesso tempo
        if (!health.getHurt())
        {
            switch (value.action.name) {
                case "Move":
                    movementChecker(value);
                    break;
                case "Jump":
                    jumpChecker(value);
                    break;
                case "Run":
                    onRun(value);
                    break;
                case "Roll":
                    onRoll(value);
                    break;
            }
        }
        else
        {
            horizontal = 0;
        }
    }

    private void movementChecker(InputAction.CallbackContext value) {
        horizontal = value.ReadValue<Vector2>().x;
    }

    private void jumpChecker(InputAction.CallbackContext v)
    {
        if (v.started && nJumps < 2 && !rollPressed)
        {
            nJumps++;
            player.velocity = new Vector2(player.velocity.x, jumpingPower);
        }
    }

    /*public void onJump(InputAction.CallbackContext v) {
        jump = v.started;
    }*/

    private void onRun(InputAction.CallbackContext v) {
        if (v.started) runPressed = v.started; else runPressed = v.action.triggered;
    }

    private void runChecker()
    {
        if (runPressed && !rollPressed)
        {
            speed = runSpeed;
        }
        else if (!rollPressed)
        {
            speed = walkSpeed;
        }
    }

    private void rollChecker()
    {
        if (rollPressed)
        {
            if (speed > walkSpeed)
            {
                speed -= 0.05f;
            }
            if (horizontal == 0)
            {
                animationRollFinished();
            }
        }
    }

    private void onRoll(InputAction.CallbackContext v)
    {
        if (v.started)
        {
            speed = runSpeed;
            rollPressed = true;
        }
        if ((horizontal == 1 || horizontal == -1) && v.action.triggered) {
            rollPressed = true;
        } else if (v.action.triggered == false && v.started == false) {
            speed = walkSpeed;
            animationRollFinished();
        }
    }

    private void rotateColliderSizeInRoll() //Usato in animazione
    {
        boxCollider.forceSendLayers = collisionWithoutEnemy.forceSendLayers;
        boxCollider.forceReceiveLayers = collisionWithoutEnemy.forceReceiveLayers;
        boxCollider.size = new Vector2(startBoxColliderSizeY + changeBoxColliderSizeInrollX, startBoxColliderSizeX + changeBoxColliderSizeInrollY);
        boxCollider.offset = new Vector2(startBoxColliderOffsetX + changeBoxColliderOffsetInrollX, startBoxColliderOffsetY + changeBoxColliderOffsetInrollY);
    }

    private void animationRollFinished() //Usato anche in animazione
    {
        rollPressed = false;
        boxCollider.offset = new Vector2(startBoxColliderOffsetX, startBoxColliderOffsetY);
        boxCollider.size = new Vector2(startBoxColliderSizeX, startBoxColliderSizeY);
        boxCollider.forceSendLayers = startBoxColliderLayerMask;
        boxCollider.forceReceiveLayers = startBoxColliderLayerMask;
    }

    public void knockbackDirection(float knockBack, float positionXToReach) {
        if (isFacingRight) {
            if (positionXToReach <= transform.position.x)
            {
                player.velocity = new Vector2((-knockBack * speed), player.velocity.y);
            }
        } else {
            if (positionXToReach >= transform.position.x)
                player.velocity = new Vector2((knockBack * speed), player.velocity.y);
        }
    }

    public bool getRollPressed() { return rollPressed; }

}