using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmor;

    [Header("Item Details")]
    public string itemName;
    public string desscription;
    public int value;
    public Sprite itemSprite;

    [Header("Item Power")]
    public int amountToChange;
    public bool affectHP, affectMP, affectStrength, affectDefense;

    [Header("Weapon/Armor Details")]
    public int waeponStrength;
    public int armorStrength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use(int charToUse)
    {
        CharStats selectorChar = GameManager.instance.playerStats[charToUse];
        if (isItem)
        {
            if (affectHP)
            {
                selectorChar.currentHP += amountToChange;
                if(selectorChar.currentHP > selectorChar.maxHP)
                {
                    selectorChar.currentHP = selectorChar.maxHP;
                }
            }
            if (affectMP)
            {
                selectorChar.currentMP += amountToChange;
                if (selectorChar.currentMP > selectorChar.maxMP)
                {
                    selectorChar.currentMP = selectorChar.maxMP;
                }
            }
            if (affectStrength)
            {
                selectorChar.strength += amountToChange;
            }
            if (affectDefense)
            {
                selectorChar.defense += amountToChange;
            }
        }
        if (isWeapon)
        {
            if(selectorChar.equippedWeapon != "")
            {
                GameManager.instance.AddItem(selectorChar.equippedWeapon);
            }
            selectorChar.equippedWeapon = itemName;
            selectorChar.weaponPower = waeponStrength;
        }
        if (isArmor)
        {
            if (selectorChar.equippedArmor != "")
            {
                GameManager.instance.AddItem(selectorChar.equippedArmor);
            }
            selectorChar.equippedArmor = itemName;
            selectorChar.armorPower = armorStrength;
        }
        GameManager.instance.RemoveItem(itemName);
    }
}
