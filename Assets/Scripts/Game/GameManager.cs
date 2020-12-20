using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
