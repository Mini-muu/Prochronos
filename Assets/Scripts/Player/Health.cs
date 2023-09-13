using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float health;
    private bool hurt = false;
    private Animator animator;

    public void incrementHealth(float add)
    {
        health += add;
    }
    public void reduceHealth(float reduce)
    {
        health -= reduce;
        hurt = true;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }

    public float getHealth()
    {
        return health;
    }

    private void removePlayer() //Usato in animazione
    {
        gameObject.SetActive(false);
    }

    private void hurtFinished() //Usato in animazione
    {
        hurt = false;
    }

    public bool getHurt() { return hurt; }

    private void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        animator.SetFloat("health", health);
    }

    public void Update()
    {
        animator.SetBool("hurt", hurt);
        animator.SetFloat("health", health);
    }
}
