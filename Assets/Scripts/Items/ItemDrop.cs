using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private List<ItemDropData> possibleDrop;
    //[SerializeField] private ItemData[] _possibleDrop;
    private readonly List<ItemData> dropList = new ();

    [SerializeField] private GameObject dropPrefab;


    public void GenerateDrop()
    {
        if (possibleDrop.Count == 0) return;

        SelectDrop();

        if(dropList.Count == 0) return;

        foreach(ItemData item in dropList)
            DropItem(item);
    }

    private void SelectDrop()
    {
        for (int i = 0; i < possibleDrop.Count; i++)
        {
            //If the random number is lower than the dropchance then the items will be dropped
           if (Random.Range(0.1f, 100f) <= possibleDrop[i].DropChance)
                dropList.Add(possibleDrop[i].GetData());
        }
    }

    public void DropItem(ItemData _itemData)
    {
        GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);

        //Item drop "Pop" Effect
        Vector2 randomVelocity = new(Random.Range(-5, 5), Random.Range(15, 20));

        newDrop.GetComponent<ItemObject>().SetupItem(_itemData, randomVelocity);
    }
}
