using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharStats[] playerStats;

    public bool gameMenuOpen, dialogActive, faddingBetweenAreas, shopActive;

    public string[] itemsHeld;
    public int[] numberOfItems;
    public Item[] referenceItems;

    public int currentGold;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        SortItems();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameMenuOpen || dialogActive || faddingBetweenAreas || shopActive)
        {
            PlayerController.instance.canMove = false;
        }
        else
        {
            PlayerController.instance.canMove = true;
        }
    }

    public Item GetItemDetails(string itemToGrab)
    {
        Item  it = null;
        for (int i = 0; i< referenceItems.Length; i++)
        {
            if(referenceItems[i].itemName == itemToGrab)
            {
                it = referenceItems[i];
            }
        }
        return it; 
    }

    public void SortItems()
    {
        bool itemAfterSpace = true;
        while (itemAfterSpace)
        {
            itemAfterSpace = false;
            for (int i = 0; i < itemsHeld.Length - 1; i++)
            {
                if (itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[i + 1];
                    itemsHeld[i + 1] = "";
                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;
                    if(itemsHeld[i] != "")
                    {
                        itemAfterSpace = true;
                    }
                }
            }
        }
    }

    public void AddItem(string itemName)
    {
        int newItemPosition = 0;
        bool foundEmpty = false;

        for(int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == "" || itemsHeld[i] == itemName)
            {
                newItemPosition = i;
                i = itemsHeld.Length;
                foundEmpty = true;
            }
        }
        if (foundEmpty)
        {
            bool itemExist = false;
            for(int i = 0; i < referenceItems.Length; i++)
            {
                if(referenceItems[i].itemName == itemName)
                {
                    itemExist = true;
                    i = referenceItems.Length;
                }
            }
            if (itemExist)
            {
                itemsHeld[newItemPosition] = itemName;
                numberOfItems[newItemPosition]++;
            }
            else
            {
                Debug.LogError(itemName+ " does not exist!");
            }
        }
        GameMenu.instance.ShowItems();
    }

    public void RemoveItem(string itemRemove)
    {
        bool foundItem = false;
        int itemPosition = 0;

        for(int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == itemRemove)
            {
                foundItem = true;
                itemPosition = i;
                i = itemsHeld.Length;
            }
        }
        if (foundItem)
        {
            numberOfItems[itemPosition]--;
            if(numberOfItems[itemPosition] <= 0)
            {
                itemsHeld[itemPosition] = "";
            }
            GameMenu.instance.ShowItems();
        }
        else
        {
            Debug.LogError(itemRemove + " does not exist!");
        }
    }

    public void SaveData()
    {
        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("Player_Position_x", PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_y", PlayerController.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_z", PlayerController.instance.transform.position.z);

        for(int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_active", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_active", 0);
            }
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_Level", playerStats[i].playerLevel);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_CurrentExp", playerStats[i].currentExp);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_CurrentHP", playerStats[i].currentHP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_MaxHP", playerStats[i].maxHP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_CurrentMP", playerStats[i].currentMP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_MaxMP", playerStats[i].maxMP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_Strength", playerStats[i].strength);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_Defense", playerStats[i].defense);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_WpnPwr", playerStats[i].weaponPower);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_ArmPwr", playerStats[i].armorPower);
            PlayerPrefs.SetString("Player_" + playerStats[i].charName + "_EquipedWpn", playerStats[i].equippedWeapon);
            PlayerPrefs.SetString("Player_" + playerStats[i].charName + "_EquipedArmr", playerStats[i].equippedArmor);
        }

        for(int j = 0; j < itemsHeld.Length; j++)
        {
            PlayerPrefs.SetString("ItemInventory_" + j, itemsHeld[j]);
            PlayerPrefs.SetInt("ItemAmount_" + j, numberOfItems[j]);
        }
    }

    public void LoadData()
    {
        PlayerController.instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_x", PlayerController.instance.transform.position.x), PlayerPrefs.GetFloat("Player_Position_y", PlayerController.instance.transform.position.y), PlayerPrefs.GetFloat("Player_Position_z", PlayerController.instance.transform.position.z));
        for(int i = 0; i < playerStats.Length; i++)
        {
            if(PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_active") == 0)
            {
                playerStats[i].gameObject.SetActive(false);
            }
            else
            {
                playerStats[i].gameObject.SetActive(true);
            }
            playerStats[i].playerLevel = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_Level");
            playerStats[i].currentExp = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_CurrentExp");
            playerStats[i].currentHP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_CurrentHP");
            playerStats[i].maxHP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_MaxHP");
            playerStats[i].currentMP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_CurrentMP");
            playerStats[i].maxMP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_MaxMP");
            playerStats[i].strength = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_Strength");
            playerStats[i].defense = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_Defense");
            playerStats[i].weaponPower = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_WpnPwr");
            playerStats[i].armorPower = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_ArmPwr");
            playerStats[i].equippedWeapon = PlayerPrefs.GetString("Player_" + playerStats[i].charName + "_EquipedWpn");
            playerStats[i].equippedArmor = PlayerPrefs.GetString("Player_" + playerStats[i].charName + "_EquipedArmr");
        }

        for(int j = 0; j < itemsHeld.Length; j++)
        {
            itemsHeld[j] = PlayerPrefs.GetString("ItemInventory_" + j);
            numberOfItems[j] = PlayerPrefs.GetInt("ItemAmount_" + j);
        }
    }

    public void pushCharToArr(CharStats charac)
    {
        int pos = 0;
        bool find = false;
        while(!find)
        {
            if(playerStats[pos] == null)
            {
                find = true;
            }
            else
            {
                pos++;
            }
        }
        playerStats[pos] = charac;
    }
}
