using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    public string charName;
    public int playerLevel = 1;
    public int currentExp;
    public int[] expToNextLevel;
    public int maxLevel = 100;
    public int baseExp = 1000;

    public int currentHP;
    public int maxHP = 1000;
    public int currentMP;
    public int maxMP = 300;
    public int strength;
    public int defense;
    public int weaponPower;
    public int armorPower;
    public string equippedWeapon;
    public string ewuippedArmor;
    public Sprite charImage;

    // Start is called before the first frame update
    void Start()
    {
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseExp;
        for (int i = 2; i < expToNextLevel.Length; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i-1] * 1.05f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddExp(500);
        }
    }

    public void AddExp(int expToAdd)
    {
        currentExp += expToAdd;
        if(playerLevel < maxLevel)
        {
            if (currentExp > expToNextLevel[playerLevel])
            {
                currentExp -= expToNextLevel[playerLevel];
                LevelUP();
            }
        }
        else
        {
            currentExp = 0;
        }
    }

    public void LevelUP()
    {
        playerLevel++;

        //determine whether to add to str or def based on add or even
        if (playerLevel % 2 == 0)
        {
            strength = Mathf.FloorToInt(strength * 1.25f);
            defense = Mathf.FloorToInt(defense * 1.05f);
            maxHP = Mathf.FloorToInt(maxHP * 1.25f);
            maxMP = Mathf.FloorToInt(maxMP * 1.05f);
        }
        else
        {
            defense = Mathf.FloorToInt(defense * 1.25f);
            strength = Mathf.FloorToInt(strength * 1.05f);
            maxHP = Mathf.FloorToInt(maxHP * 1.05f);
            maxMP = Mathf.FloorToInt(maxMP * 1.25f);
        }
        currentHP = maxHP;
        currentMP = maxMP;
    }
}
