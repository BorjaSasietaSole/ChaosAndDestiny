using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject theMenu;
    public GameObject[] windows;

    private static CharStats[] playerStats;

    public Text[] nameText, hpText, mpText, expText, lvlText;
    public Slider[] expSlider;
    public Image[] charImage;
    public GameObject[] charStatHolder;

    public GameObject[] statusButtons;

    public Text statusName, statusHP, statusMP, statusStrength, statusDef, statusWpnEquip, statusWpnPow, statusArmorEqup, statusArmPow, statusExp;
    public Image statusImage;

    public ItemButton[] itemButtons;
    public string selectedItem;
    public Item activeItem;
    public Text itemName, itemDescription, useButtonText;

    public GameObject itemCharChoiceMenu;
    public Text[] itemCharChoiceName;

    public static GameMenu instance;
    public Text GoldText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (theMenu.activeInHierarchy)
            {
                //theMenu.SetActive(false);
                //GameManager.instance.gameMenuOpen = false;
                CloseMenu();
            }
            else
            {
                theMenu.SetActive(true);
                UpdateMainStats();
                GameManager.instance.gameMenuOpen = true;
            }
            AudioManager.instance.PlaySFX(5);
        }
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;

        for(int i = 0; i < playerStats.Length; i++)
        {
            if(playerStats[i] != null && playerStats[i].gameObject.activeSelf)
            {
                charStatHolder[i].SetActive(true);
                UpdateCharacterStats(i);
            }
        }
        GoldText.text = GameManager.instance.currentGold.ToString() + "$";
    }

    public void UpdateCharacterStats(int pos)
    {
        nameText[pos].text = playerStats[pos].charName;
        hpText[pos].text = "HP: " + playerStats[pos].currentHP + "/" + playerStats[pos].maxHP;
        mpText[pos].text = "MP: " + playerStats[pos].currentMP + "/" + playerStats[pos].maxMP;
        lvlText[pos].text = "Level: " + playerStats[pos].playerLevel;
        expText[pos].text = playerStats[pos].currentExp + "/" + playerStats[pos].expToNextLevel[playerStats[pos].playerLevel];
        expSlider[pos].maxValue = playerStats[pos].expToNextLevel[playerStats[pos].playerLevel];
        expSlider[pos].value = playerStats[pos].currentExp;
        charImage[pos].sprite = playerStats[pos].charImage;
    }

    public void ToggleWindow(int windowNumber)
    {
        for(int i = 0; i < windows.Length; i++)
        {
            if(i == windowNumber)
            {
                if (windows[i].activeInHierarchy)
                {
                    windows[i].SetActive(false);
                }
                else
                {
                    windows[i].SetActive(true);
                }
            }
            else
            {
                windows[i].SetActive(false);
            }
        }
        itemCharChoiceMenu.SetActive(false);
    }

    public void CloseMenu()
    {
        for(int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }
        theMenu.SetActive(false);
        GameManager.instance.gameMenuOpen = false;
        itemCharChoiceMenu.SetActive(false);
    }

    public void OpenStatus()
    {
        UpdateMainStats();
        StatusCharacter(0);
        for(int i = 0; i < statusButtons.Length; i++)
        {
            statusButtons[i].SetActive(playerStats[i].gameObject.activeSelf);
            statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].charName;
        }
    }

    public void StatusCharacter(int selected)
    {
        statusName.text = "Name: " + playerStats[selected].charName;
        statusHP.text = "HP: " + playerStats[selected].currentHP + "/" + playerStats[selected].maxHP;
        statusMP.text = "MP: " + playerStats[selected].currentMP + "/" + playerStats[selected].maxMP;
        statusStrength.text = "Strength: " + playerStats[selected].strength;
        statusDef.text = "Defense: " + playerStats[selected].defense;
        statusWpnEquip.text = "Equiped weapon: " + playerStats[selected].equippedWeapon;
        statusWpnPow.text = "Weapon power: " + playerStats[selected].weaponPower;
        statusArmorEqup.text = "Equiped armor: " + playerStats[selected].equippedArmor;
        statusArmPow.text = "Armor Defense: " + playerStats[selected].armorPower;
        statusExp.text = "Exp to next level: " + (playerStats[selected].expToNextLevel[playerStats[selected].playerLevel] - playerStats[selected].currentExp);
        statusImage.sprite = playerStats[selected].charImage;
    }

    public void ShowItems()
    {
        GameManager.instance.SortItems();
        for(int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;
            if (GameManager.instance.itemsHeld[i] != "")
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }

    public void SelectItem(Item newItem)
    {
        activeItem = newItem;
        if (activeItem.isItem)
        {
            useButtonText.text = "Use";
        }
        if(activeItem.isWeapon || activeItem.isArmor)
        {
            useButtonText.text = "Equip";
        }
        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.desscription;
    }

    public void DiscartItem()
    {
        if(activeItem != null)
        {
            GameManager.instance.RemoveItem(activeItem.itemName);
        }
    }

    public void OpenItemCharChoice()
    {
        itemCharChoiceMenu.SetActive(true);
        for(int i = 0; i < itemCharChoiceName.Length; i++)
        {
            if(GameManager.instance.playerStats[i] != null)
            {
                itemCharChoiceName[i].text = GameManager.instance.playerStats[i].charName;
                itemCharChoiceName[i].transform.parent.gameObject.SetActive(true);
            }
        }
    }

    public void CloseItemCharChoice()
    {
        itemCharChoiceMenu.SetActive(false);
    }

    public void UseItem(int selectChar)
    {
        activeItem.Use(selectChar);
        CloseItemCharChoice();
    }

    public void SaveGame()
    {
        GameManager.instance.SaveData();
    }

    public void PlayButtonSound()
    {
        AudioManager.instance.PlaySFX(4);
    }
    public void pushCharToArr(GameObject charac)
    {
        int pos = 0;
        bool find = false;
        while (!find)
        {
            if (charStatHolder[pos] == charac)
            {
                find = true;
                charStatHolder[pos].SetActive(true);
            }
            else
            {
                pos++;
            }
        }
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Menu Principal");
        Destroy(GameManager.instance.gameObject);
        Destroy(PlayerController.instance.gameObject);
        Destroy(AudioManager.instance.gameObject);
        Destroy(gameObject);
    }
}
