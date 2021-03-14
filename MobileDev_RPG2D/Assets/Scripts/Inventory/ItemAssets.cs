using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : Singleton<ItemAssets>
{
    public Transform worldItem;

    [SerializeField] public Sprite sword;
    [SerializeField] public Sprite shield;
    [SerializeField] public Sprite bow;

    [SerializeField] public Sprite helmet;
    [SerializeField] public Sprite chestPeice;
    [SerializeField] public Sprite legArmour;
    [SerializeField] public Sprite feetArmour;

    [SerializeField] public Sprite healthPotion;
    [SerializeField] public Sprite healthFlask;
    [SerializeField] public Sprite specialPotion;
    [SerializeField] public Sprite gold;
}
