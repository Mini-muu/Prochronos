using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//https://docs.unity3d.com/ScriptReference/Transform.SetAsLastSibling.html
public class UI_PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Sprite intactHPSprite;
    [SerializeField] private Sprite damagedfHPSprite;
    [SerializeField] private Sprite brokenHPSprite;

    [SerializeField] private Transform firstSprite;
    private Transform parentObject;

    private PlayerStats playerStats;
    private List<Transform> hpIndicators;

    private void Start()
    {
        playerStats = PlayerManager.instance.playerStats;
        playerStats.onHealthChanged += UpdateHealthUI;
        playerStats.onMaxHealthChanged += UpdateMaxHealthUI;

        parentObject = transform;

        hpIndicators = new List<Transform>
        {
            firstSprite
        };

        UpdateMaxHealthUI();
    }

    private void UpdateHealthUI()
    {
        int currentHp = playerStats.currentHealth;

        //MaxHP = 5 -> 3 Slot
        //currentHP = 3 -> 1.5 slot
        for (int i = 0; i < hpIndicators.Count; i++)
        {
            hpIndicators[i].GetComponent<Image>().sprite = currentHp > (i + 1) * 2 ? intactHPSprite : (i+1)*2 - currentHp >= 2 ? brokenHPSprite : currentHp % 2 == 0 ? intactHPSprite : damagedfHPSprite;
            //i = 1 -> 3 > 2(i+1) [y] -> intact
            //i = 2 -> 3 > 4(i+1) [n] -> 4 - 3 >= 2 [n] -> 3 % 2 == 0 [n] -> damaged
            //i = 3 -> 3 > 6(i+1) [n] -> 6 - 3 >= 2 [y] -> broken
            //
            //i = 1 -> 4 > 2(i+1) [y] -> intact
            //i = 2 -> 4 > 4(i+1) [n] -> 4 - 4(actual) >= 2 [n] -> 4(actual) % 2 == 0 [y] -> intact
        }
    }

    private void UpdateMaxHealthUI()
    {
        while(playerStats.maxHealth.GetValue()/2.0 > hpIndicators.Count)
        {
            Transform clone = Instantiate(firstSprite, parentObject);
            clone.SetAsLastSibling();
            hpIndicators.Add(clone);
        }

        UpdateHealthUI();
    }

    /*
        1 2  3 4  5 6  7 8   
        | |  | |  | |  | | ...             
         1    2    3    4

        Index = HP % 2 == 0 ? HP / 2 : HP/2+1;
        UpdateAll() => foreach -> ...
     */

}