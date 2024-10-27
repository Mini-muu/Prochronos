using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private ItemData[] possibleDrop;
    private readonly List<ItemData> dropList = new ();

    [SerializeField] private GameObject dropPrefab;

    public void GenerateDrop()
    {
        if (possibleDrop.Length == 0) return;

        SelectDrop();

        if(dropList.Count == 0) return;

        foreach(ItemData item in dropList)
            DropItem(item);
    }

    private void SelectDrop()
    {
        for (int i = 0; i < possibleDrop.Length; i++)
        {
            //If the random number is lower than the dropchance then the items will be dropped
           if (Random.Range(0.1f, 100f) <= possibleDrop[i].dropChance)
                dropList.Add(possibleDrop[i]);
        }
    }

    public void DropItem(ItemData _itemData)
    {
        GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);

        //Item drop "Pop" Effect
        Vector2 randomVelocity = new(Random.Range(-5, 5), Random.Range(15, 20));

        newDrop.GetComponent<ItemObject>().SetupItem(_itemData, randomVelocity);
    }

    //TODO - Remove
    public void TutorialDropGenerator(bool isBones)
    {
        if (isBones)
        {
            TutorialDrop(possibleDrop[0]);
        }
        else
        {
            TutorialDrop(possibleDrop[1]);
        }
    }

    //TODO - Remove
    private void TutorialDrop(ItemData _itemData)
    {
        GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);

        Vector2 randomVelocity = new(14, Random.Range(15, 20));

        newDrop.GetComponent<ItemObject>().SetupItem(_itemData, randomVelocity);
    }
}
