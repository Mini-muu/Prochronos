using UnityEngine;

public class BaseInteraction : MonoBehaviour
{
    public bool IsKeyInteractionNeeded;
    [Space]
    [Header("Temporaneo")]
    public KeyCode interactionKey;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (IsKeyInteractionNeeded && Input.GetKeyDown(interactionKey))
                ExecuteInteraction();
            else
                ExecuteInteraction();
        }
    }

    public virtual void ExecuteInteraction() 
    {

    }

    //Type interaction
    //Monoliti -> Interagibile
    //Cartelli -> Interagibile
    //Oggetti ambientali -> Interagibile
    //Albero Mercante -> Attaccabile (Pesante)
    //Fal� -> Entrare nell'area specificata
}
