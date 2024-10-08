using UnityEngine;

public class BaseInteraction : MonoBehaviour
{
    public bool IsKeyPressNeeded;
    //[Space]
    //[Header("Temporaneo")]
    //public KeyCode interactionKey;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            //replace with performed
            if (IsKeyPressNeeded && CheckKeyPress())//Input.GetKeyDown(interactionKey))
                ExecuteInteraction();
            else
                ExecuteInteraction();
        }
    }

    private bool CheckKeyPress() => PlayerInputManager.instance.interact.IsPressed();


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
