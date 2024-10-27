using UnityEngine;

[CreateAssetMenu(fileName = "Bones Item Data", menuName = "Data/Bones")]
public class BonesData : ItemData
{
    public int DroppedAmount { get; private set; }
    [SerializeField, Min(1)] private int minAmount = 1;
    [SerializeField, Min(1)] private int maxAmount = 1;

    public void GenerateAmount()
    {
        DroppedAmount = Random.Range(minAmount, maxAmount + 1);
    }
}
