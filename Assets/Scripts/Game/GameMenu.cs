using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text statusName, statusHP, statusMP;

    // Start is called before the first frame update
    void Start()
    {
        
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
        }
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;

        for(int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                charStatHolder[i].SetActive(true);
                UpdateCharacterStats(i);
            }
            else
            {
                charStatHolder[i].SetActive(false);
            }
        }
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
                windows[i].SetActive(windows[i].activeInHierarchy);
            }
            else
            {
                windows[i].SetActive(false);
            }
        }
    }

    public void CloseMenu()
    {
        for(int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }
        theMenu.SetActive(false);
        GameManager.instance.gameMenuOpen = false;
    }

    public void OpenStatus()
    {
        UpdateMainStats();
        for(int i = 0; i < statusButtons.Length; i++)
        {
            statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].charName;
        }
    }

    public void StatusCharacter(int selected)
    {

    }
}
