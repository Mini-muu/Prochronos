using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Boh
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;

    [Header("Stats Modifiers")]
    public int health;
    public int stamina;
    public int armor;
    public int damage;
    public int strongDamage;

    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.playerStats;

        playerStats.maxHealth.AddModifier(health);
        playerStats.maxStamina.AddModifier(stamina);
        playerStats.armor.AddModifier(armor);
        playerStats.damage.AddModifier(damage);
        playerStats.strongDamage.AddModifier(strongDamage);
    }

    public void RemoveModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.playerStats;

        playerStats.maxHealth.RemoveModifier(health);
        playerStats.maxStamina.RemoveModifier(stamina);
        playerStats.armor.RemoveModifier(armor);
        playerStats.damage.RemoveModifier(damage);
        playerStats.strongDamage.RemoveModifier(strongDamage);
    }
}
