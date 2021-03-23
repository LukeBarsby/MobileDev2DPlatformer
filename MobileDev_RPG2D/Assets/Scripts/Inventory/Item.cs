using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
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
    public enum Materials
    {
        Wood,
        Iron,
        Gold,
        Leather
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
    public Materials material;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            case ItemType.WoodSword:
                return ItemAssets.Instance.woodSword;
            case ItemType.WoodShield:
                return ItemAssets.Instance.woodShield;
            case ItemType.WoodBow:
                return ItemAssets.Instance.woodBow;
            case ItemType.IronSword:
                return ItemAssets.Instance.ironSword;
            case ItemType.IronShield:
                return ItemAssets.Instance.ironShield;
            case ItemType.IronBow:
                return ItemAssets.Instance.ironBow;
            case ItemType.LeatherHelmet:
                return ItemAssets.Instance.leatherHelmet;
            case ItemType.LeatherChestPiece:
                return ItemAssets.Instance.leatherChestPeice;
            case ItemType.LeatherLegs:
                return ItemAssets.Instance.leatherLegArmour;
            case ItemType.LeatherFeet:
                return ItemAssets.Instance.leatherFeetArmour;
            case ItemType.IronHelmet:
                return ItemAssets.Instance.ironHelmet;
            case ItemType.IronChestPiece:
                return ItemAssets.Instance.ironChestPeice;
            case ItemType.IronLegs:
                return ItemAssets.Instance.ironLegArmour;
            case ItemType.IronFeet:
                return ItemAssets.Instance.ironFeetArmour;
            case ItemType.HealthPotion:
                return ItemAssets.Instance.healthPotion;
            case ItemType.Gold:
                return ItemAssets.Instance.gold;
            default:
                return ItemAssets.Instance.woodSword;
        }
    }

    public Material GetMaterial()
    {
        switch (material)
        {
            case Materials.Wood:
                return ItemAssets.Instance.woodMaterial;
            case Materials.Iron:
                return ItemAssets.Instance.ironMaterial;
            case Materials.Gold:
                return ItemAssets.Instance.goldMaterial;
            case Materials.Leather:
                return ItemAssets.Instance.leatherMaterial;
            default:
                return ItemAssets.Instance.woodMaterial;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            case ItemType.Gold:
            case ItemType.HealthPotion:
                return true;
            case ItemType.WoodSword:
            case ItemType.IronSword:
            case ItemType.WoodShield:
            case ItemType.IronShield:
            case ItemType.WoodBow:
            case ItemType.IronBow:
            case ItemType.LeatherHelmet:
            case ItemType.LeatherChestPiece:
            case ItemType.LeatherLegs:
            case ItemType.LeatherFeet:
            case ItemType.IronHelmet:
            case ItemType.IronChestPiece:
            case ItemType.IronLegs:
            case ItemType.IronFeet:
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
            case ItemType.WoodSword:
            case ItemType.IronSword:
            case ItemType.WoodShield:
            case ItemType.IronShield:
            case ItemType.WoodBow:
            case ItemType.IronBow:
            case ItemType.LeatherHelmet:
            case ItemType.LeatherChestPiece:
            case ItemType.LeatherLegs:
            case ItemType.LeatherFeet:
            case ItemType.IronHelmet:
            case ItemType.IronChestPiece:
            case ItemType.IronLegs:
            case ItemType.IronFeet:
                return true;
            default:
                return false;
        }
    }
}
