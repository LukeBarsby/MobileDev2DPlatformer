using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Materials
{
    Wood,
    Iron,
    Gold,
    Leather
}

[System.Serializable]
public enum ItemType
{
    WoodSword,
    WoodShield,
    WoodBow,
    IronSword,
    IronShield,
    IronBow,
    LeatherHelmet,
    LeatherChestPiece,
    LeatherLegs,
    LeatherFeet,
    IronHelmet,
    IronChestPiece,
    IronLegs,
    IronFeet,
    HealthPotion,
    Gold,
}
[System.Serializable]
public class InventoryItemData
{
    // Generic
    public ItemType itemType;
    public int amount;
    public bool stackable;
    public bool isArmour;
    public string disription;
    //weapons
    public float damage;
    public float knockback;
    //armour
    public float defence;
    //
    public Materials itemMaterial;

}
