using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float direction;
    [Header("Enemy:")]
    [SerializeField] private Object enemy;
    private Rigidbody2D enemyRigidBody;
    private Animator animator;
    private EnemyAttack enemyAttack;
    private EnemyHealth health;
    [SerializeField] private Transform enemyTranform;
    [SerializeField] private BoxCollider2D boxCollider;
    [Header("Moovement parameters:")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float startDirection = 1;
    [SerializeField] private LayerMask groundLayer;
    [Header("Idle Behaviour:")]
    [SerializeField] private float idleDuration;
    private float feetRectHeight = 0.1f;

    private float previousDirection;
    private float speed;
    private int nJumps = 0;
    [SerializeField] float checkSizeX = 0.5f;
    //Collider2D bottomTerrain;

    [SerializeField, Tooltip("This parameter allows you to set the power of the current enemy's jump. So it can be used with the gravity scale to change the speed " +
        "of the jump")] private float jumpingPower = 9;

    private void Awake()
    {
        animator = enemy.GetComponent<Animator>();
        enemyAttack = enemy.GetComponent<EnemyAttack>();
        enemyRigidBody = enemy.GetComponent<Rigidbody2D>();
        health = enemy.GetComponent<EnemyHealth>();
    }

    private void Start()
    {
        direction = startDirection;
        previousDirection = direction;
        speed = walkSpeed;
    }

    private void Update()
    {
        if (enemyAttack.getInAttack() || health.getHurt() || health.getHealth() <= 0)
        {
            direction = 0;
        }
        animator.SetFloat("Direction", Mathf.Abs(direction));
        animator.SetBool("falling", !checkBottomTerrain());
        checkMovement();
        flip();
    }

    private void FixedUpdate()
    {
        enemyRigidBody.velocity = new Vector2(direction * speed, enemyRigidBody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.collider+"\n"+ "checkBottomTerrain: " + checkBottomTerrain());
        if (LayerMask.GetMask(LayerMask.LayerToName(collision.gameObject.layer)) == groundLayer.value && checkBottomTerrain())
        {
            nJumps = 0;
        }

        /*if (collision.collider.tag == "Terrain" &&
            (int)(collision.collider.bounds.center.y - (collision.collider.bounds.size.y / 2)) < (int)(boxCollider.bounds.center.y + (boxCollider.bounds.size.y / 2)) &&
            ((int)(collision.collider.bounds.center.y - (collision.collider.bounds.size.y / 2)) + 2) >
            (int)(boxCollider.bounds.center.y + (boxCollider.bounds.size.y / 2))
            )
        {
            Debug.Log(nJumps);
            nJumps = 0;
        }*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(boxCollider.bounds.center.x + enemyTranform.localScale.x * (boxCollider.bounds.size.x/2 + checkSizeX/2), boxCollider.bounds.center.y, boxCollider.bounds.center.z),
            new Vector3(checkSizeX, boxCollider.bounds.size.y, boxCollider.bounds.size.z));

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(new Vector3(boxCollider.bounds.center.x,boxCollider.bounds.center.y - (boxCollider.bounds.size.y / 2) - (feetRectHeight / 2), boxCollider.bounds.center.z),
                new Vector3(boxCollider.bounds.size.x, feetRectHeight, boxCollider.bounds.size.z));
    }

    private bool checkBottomTerrain()
    {
        RaycastHit2D hit = Physics2D.BoxCast(new Vector3(boxCollider.bounds.center.x, boxCollider.bounds.center.y - (boxCollider.bounds.size.y / 2) - (feetRectHeight / 2), boxCollider.bounds.center.z),
                new Vector3(boxCollider.bounds.size.x, feetRectHeight, boxCollider.bounds.size.z),
                    0, Vector2.right, 0, groundLayer);
        if (hit.collider != null)
        {
            //bottomTerrain = hit.collider;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void checkMovement()
    {
        RaycastHit2D hit = Physics2D.BoxCast(new Vector3(boxCollider.bounds.center.x + enemyTranform.localScale.x * (boxCollider.bounds.size.x / 2 + checkSizeX / 2), boxCollider.bounds.center.y, boxCollider.bounds.center.z),
            new Vector3(checkSizeX, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.right, 0, groundLayer);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Terrain")
            {
                //Debug.Log(hit.collider.name+": "+(hit.collider.bounds.center.y + hit.collider.bounds.size.y / 2) + "\n"+(boxCollider.bounds.center.y - boxCollider.bounds.size.y / 2)+"\n"+((hit.collider.bounds.center.y + hit.collider.bounds.size.y / 2) > (boxCollider.bounds.center.y - boxCollider.bounds.size.y / 2)));
                if ((hit.collider.bounds.center.y + hit.collider.bounds.size.y/2) > (boxCollider.bounds.center.y - boxCollider.bounds.size.y/2 + 0.1f)) {
                    if (nJumps < 2 && (int)enemyRigidBody.velocity.y == 0)
                    {
                        nJumps++;
                        enemyRigidBody.velocity = new Vector2(enemyRigidBody.velocity.x, jumpingPower);
                    }/*else if ((int)enemyRigidBody.velocity.y <= 0)
                    {
                        changeDirection();
                        Debug.Log("Change in jump");
                    }*/
                    /*else if (nJumps == 2)
                    {
                        changeDirection();
                        //Debug.Log("Hit size y: "+hit.collider.bounds.size.y+", "+(boxCollider.size.y*2)+" "+(hit.collider.bounds.size.y < boxCollider.size.y * 2));
                    }*/
                }
            } else if (hit.collider.tag == "Wall")
            {
                changeDirection();
            }
        }
        //Debug.Log(nJumps+", "+ ((int)enemyRigidBody.velocity.y));
    }

    private void flip()
    {
        if ((direction > 0 && transform.localScale.x <= 0) || (direction < 0 && transform.localScale.x >= 0))
            enemyTranform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void walk()
    {
        direction = previousDirection;
        setSpeed(walkSpeed);
    }

    public void followItem(float x)
    {
        if (x > transform.position.x)
        {
            direction = startDirection;
        }
        else
        {
            direction = -startDirection;
        }
        setSpeed(runSpeed);
        previousDirection = direction;
    }

    /*public void followItem(float x, float y)
    {
        if (x > transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        setSpeed(runSpeed);
    }*/

    private void changeDirection()
    {
        direction *= -1;
        previousDirection = direction;
    }

    public float getDirection()
    {
        return direction;
    }

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public float getSpeed() { return speed; }

    public float getEnemyJumpingPower()
    {
        return jumpingPower;
    }
}
