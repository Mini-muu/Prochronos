using UnityEngine;

[CreateAssetMenu(fileName ="Bones Item Data", menuName ="Data/Bones")]
public class BonesData : ItemData
{
    public int amount;

    public void GenerateAmount(bool isBoss)
    {
        if(isBoss)
        {
            amount = 666;
        } else
        {
            amount = Random.Range(2, 5);
        }
    }
}
