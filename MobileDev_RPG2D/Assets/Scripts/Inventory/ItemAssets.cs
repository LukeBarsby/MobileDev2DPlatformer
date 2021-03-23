using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : Singleton<ItemAssets>
{
    public Transform worldItem;
    [Header("Weapons")]
    [SerializeField] public Sprite woodSword;
    [SerializeField] public Sprite woodShield;
    [SerializeField] public Sprite woodBow;
    [SerializeField] public Sprite ironSword;
    [SerializeField] public Sprite ironShield;
    [SerializeField] public Sprite ironBow;

    [Header("Armour")]
    [SerializeField] public Sprite leatherHelmet;
    [SerializeField] public Sprite leatherChestPeice;
    [SerializeField] public Sprite leatherLegArmour;
    [SerializeField] public Sprite leatherFeetArmour;
    [SerializeField] public Sprite ironHelmet;
    [SerializeField] public Sprite ironChestPeice;
    [SerializeField] public Sprite ironLegArmour;
    [SerializeField] public Sprite ironFeetArmour;

    [Header("Items")]
    [SerializeField] public Sprite healthPotion;
    [SerializeField] public Sprite gold;

    [Header("Materials")]
    [SerializeField] public Material woodMaterial;
    [SerializeField] public Material ironMaterial;
    [SerializeField] public Material goldMaterial;
    [SerializeField] public Material leatherMaterial;

}
