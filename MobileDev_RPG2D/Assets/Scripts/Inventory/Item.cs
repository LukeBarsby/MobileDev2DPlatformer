using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
    public enum ItemType
    {
        Sword, 
        Shield,
        Bow,
        HealthPotion,
        Gold,
        Helmet,
        ChestPiece,
        Legs,
        Feet
    }
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
    //generic equpment
    public Material material;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            case ItemType.Sword:
                return ItemAssets.Instance.sword;
            case ItemType.Shield:
                return ItemAssets.Instance.shield;
            case ItemType.Bow:
                return ItemAssets.Instance.bow;
            case ItemType.HealthPotion:
                return ItemAssets.Instance.healthPotion;
            case ItemType.Gold:
                return ItemAssets.Instance.gold;
            case ItemType.Helmet:
                return ItemAssets.Instance.helmet;
            case ItemType.ChestPiece:
                return ItemAssets.Instance.chestPeice;
            case ItemType.Legs:
                return ItemAssets.Instance.legArmour;
            case ItemType.Feet:
                return ItemAssets.Instance.feetArmour;
            default:
                return ItemAssets.Instance.sword;
        }
    }
    public bool IsStackable()
    {
        switch (itemType)
        {
            case ItemType.Gold:
            case ItemType.HealthPotion:
                return true;
            case ItemType.Sword:
            case ItemType.Shield:
            case ItemType.Bow:
            case ItemType.Helmet:
            case ItemType.ChestPiece:
            case ItemType.Legs:
            case ItemType.Feet:
                return false;
            default:
                return false;
        }
    }
    public bool PopupWindowNeeded()
    {
        switch (itemType)
        {
            case ItemType.Gold:
                return false;
            case ItemType.HealthPotion:
            case ItemType.Sword:
            case ItemType.Shield:
            case ItemType.Bow:
            case ItemType.Helmet:
            case ItemType.ChestPiece:
            case ItemType.Legs:
            case ItemType.Feet:
                return true;
            default:
                return false;
        }
    }
}
