using UnityEngine;

[CreateAssetMenu(fileName = "New weapon", menuName = "Weapon")]
public class Weapon : ItemEquipment
{
    public enum DamageTypeEnum
    {
        Normal = 0,
        Arcane = 1,
        Water = 2,
        Fire = 3
    }

    public enum RangeEnum
    {
        Small = 0,
        Middle = 1,
        Large = 2
    }

    public enum SpeedEnum
    {
        Fast = 0,
        Normal = 1,
        Slow = 2
    }
    
    public DamageTypeEnum DamageType;

    public uint minDamage;
    public uint maxDamage;

    public RangeEnum Range;

    public SpeedEnum Speed;

    public uint staminaConsume;

    public int GetDamage()
    {
        return (int) Random.Range(minDamage, maxDamage + 1);
    }

    public string GetName()
    {
        return itemName;
    }
}