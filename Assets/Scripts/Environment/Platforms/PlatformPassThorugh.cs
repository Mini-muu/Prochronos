using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPassThorugh : MonoBehaviour
{
    private Collider2D collider;
    private bool isPlayerOnPlatform;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if(isPlayerOnPlatform && Input.GetAxisRaw("Vertical") < 0 && IsPassThroughUnlocked())
        {
            collider.enabled = false;
            StartCoroutine(EnableCollider());
        }
    }

    private bool IsPassThroughUnlocked() => PlayerManager.instance.unlockedActions.Contains(PlayerAction.PlatfromPassThrough);

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        collider.enabled = true;
    }

    private void SetPlayerOnPlatoform(Collision2D _collision, bool value)
    {
        Player player = _collision.gameObject.GetComponent<Player>();
        if (player != null)
            isPlayerOnPlatform = value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SetPlayerOnPlatoform(collision, true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        SetPlayerOnPlatoform(collision, false);
    }
}
