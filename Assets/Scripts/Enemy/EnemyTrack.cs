using UnityEngine;

public class EnemyTrack : MonoBehaviour
{
    [Header("Track limits:")] //Header: permette di creare una raccolta di richieste presenti nel suddetto componente una volta presente nell'inspector, aiutando, quindi, ad organizzare meglio le richieste di tipo [SerializeField] quando vengono visualizzate.
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    private float idleTimer;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /*private void DirectionChange()
    {
        if (getMovementX() != 0 && idleTimer > idleDuration)
        {
            direction = 0;
            idleTimer += Time.deltaTime;
        }
        else
        {
            direction = -previousDirection;
            idleTimer = 0;
        }
    }*/
}
