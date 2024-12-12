using UnityEngine;

public class Fungus : MonoBehaviour
{
    [Header("Fungus Info")]
    public int damage = 3;

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