using TMPro;
using UnityEngine;

public class Sign : MonoBehaviour
{
    [SerializeField] private SignData data;
    [SerializeField] private TextMeshPro signText;

    private void OnValidate()
    {
        if(data == null) return;

        Setup();
    }

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        gameObject.name = $"Sign - {data.SignName}";
        GetComponent<Collider2D>().isTrigger = true;
        signText = GetComponentInChildren<TextMeshPro>();

        signText.text = data.SignText;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            data.UnlockActions();
        }
    }
}