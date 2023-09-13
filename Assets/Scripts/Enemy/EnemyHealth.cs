using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField, Tooltip("Maximum health value that could be genereated during the generation")] private float maxHealth = 6;
    private float health;
    private bool hurt = false;
    private Animator animator;
    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyMovement = GetComponentInParent<EnemyMovement>();
        enemyAttack = GetComponentInParent<EnemyAttack>();
        health = (int) (Random.value * maxHealth)+1;
        Debug.Log("Enemy "+gameObject.name+" health: "+health);
    }

    public void Update()
    {
        animator.SetBool("hurt", hurt);
        animator.SetFloat("health", health);
    }

    public void setHealth(float newHealth)
    {
        health = newHealth;
    }
    public float getHealth()
    {
        return health;
    }
    public void reduceHealth(float reduce)
    {
        health -= reduce;
        if (health <= 0)
        {
            enemyMovement.setSpeed(0);
        } else
        {
            hurt = true;
            enemyAttack.setInAttack(false);
        }
    }

    private void hurtFinished() //Metodo in animation
    {
        hurt = false;
    }

    private void removeEnemy() //Metodo in animation
    {
        gameObject.SetActive(false);
    }

    public bool getHurt() { return hurt; }
}
