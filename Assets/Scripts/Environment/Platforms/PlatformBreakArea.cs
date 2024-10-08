using UnityEngine;

public class PlatformBreakArea : MonoBehaviour
{
    [SerializeField] BoxCollider2D platformCollider;
    [SerializeField] SpriteRenderer platformSpriteRenderer;

    public bool IsInArea(Collider2D collider) => collider.GetComponent<Player>() != null;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (IsInArea(collider))
        {
            platformCollider.enabled = false;
            platformSpriteRenderer.color = Color.red;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}

