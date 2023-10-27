using UnityEngine;

public class Thorn : MonoBehaviour
{
    [SerializeField]  private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private Health playerHealth;
    private float damage = 0.5f;
    private float disabledTrapTime = 3;
    private float disableTime;

    // Start is called before the first frame update
    void Start()
    {
        disableTime = disabledTrapTime;
    }

    // Update is called once per frame
    void Update()
    {
        checkCharacter();
    }

    private void checkCharacter()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center,
                boxCollider.bounds.size,
                    0, Vector2.right, 0, playerLayer);
        if (hit.collider != null)
        {
            if (disableTime == disabledTrapTime)
            {
                playerHealth = hit.transform.gameObject.GetComponent<Health>();
                playerHealth.reduceHealth(damage);
                disableTime -= Time.deltaTime;
            } else
            {
                disableTime -= Time.deltaTime;
                if (disableTime <= 0) disableTime = disabledTrapTime;
            }
        } else
        {
            disableTime = disabledTrapTime;
        }
    }
}
