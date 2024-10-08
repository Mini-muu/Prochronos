using UnityEngine;

public class Trap : MonoBehaviour
{
    [Header("Trap Info")]
    public int damage = 1;
    public bool doesKnockback = true;
    public Vector2 knockback;

    protected float timer;

    // Use this for initialization
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        timer -= Time.deltaTime;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

    }
}