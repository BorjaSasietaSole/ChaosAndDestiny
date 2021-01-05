﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    private bool battleActive;

    public GameObject battleScene;

    public Transform[] playersPositions, enemyPositions;

    public BattleChar[] playerPrefabs, enemyPrefabs;

    public List<BattleChar> activeBattlers = new List<BattleChar>();

    public int currentTurn;
    public bool turnWaiting;

    public GameObject uiButtonsHolder;

    public BattleMove[] moveList;
    public GameObject enemyAtteckEffect;

    public DamageNumber theDamageNumber;

    public Text[] playerNames, playerHP, playerMP;

    public GameObject targetMenu;
    public BattleTargetButton[] targetButtons;

    public GameObject magicMenu;
    public BattleMagicSelect[] magicButtons;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            BattleStart(new string[] { "Shelleye", "Grey Tarantula" });
        }
        if (battleActive)
        {
            if (turnWaiting)
            {
                if (activeBattlers[currentTurn].isPlayer)
                {
                    uiButtonsHolder.SetActive(true);
                }
                else
                {
                    uiButtonsHolder.SetActive(false);
                    StartCoroutine(EnemyMoveCo());
                }
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                NextTurn();
            }
        }
    }

    public void BattleStart(string[] enemiesToSpawn)
    {
        if (!battleActive)
        {
            battleActive = true;
            GameManager.instance.battleActive = true;
            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
            battleScene.SetActive(true);
            AudioManager.instance.PlayBGM(0);
            for(int i = 0; i < playersPositions.Length; i++)
            {
                if (GameManager.instance.playerStats[i].gameObject.activeInHierarchy)
                {
                    for(int j = 0; j < playerPrefabs.Length; j++)
                    {
                        if(playerPrefabs[j].charName == GameManager.instance.playerStats[i].charName)
                        {
                            BattleChar newPlayer = Instantiate(playerPrefabs[j], playersPositions[i].position, playersPositions[i].rotation);
                            newPlayer.transform.parent = playersPositions[i];
                            activeBattlers.Add(newPlayer);
                            CharStats thePlayer = GameManager.instance.playerStats[i];
                            activeBattlers[i].currentHP = thePlayer.currentHP;
                            activeBattlers[i].currentMP = thePlayer.currentMP;
                            activeBattlers[i].maxHP = thePlayer.maxHP;
                            activeBattlers[i].maxMP = thePlayer.maxMP;
                            activeBattlers[i].strength = thePlayer.strength;
                            activeBattlers[i].defence = thePlayer.defense;
                            activeBattlers[i].wpnPower = thePlayer.weaponPower;
                            activeBattlers[i].armrPower = thePlayer.armorPower;
                        }
                    }
                }
            }
            for(int k = 0; k < enemiesToSpawn.Length; k++)
            {
                if(enemiesToSpawn[k] != "" && enemiesToSpawn[k] != null)
                {
                    for(int m = 0; m < enemyPrefabs.Length; m++)
                    {
                        if(enemyPrefabs[m].charName == enemiesToSpawn[k])
                        {
                            BattleChar newEnemy = Instantiate(enemyPrefabs[m], enemyPositions[k].position, enemyPositions[k].rotation);
                            newEnemy.transform.parent = enemyPositions[k];
                            activeBattlers.Add(newEnemy);
                        }
                    }
                }
            }
            turnWaiting = true;
            currentTurn = Random.Range(0, activeBattlers.Count);
            UpdateUIStats();
        }
    }
    public void NextTurn()
    {
        currentTurn++;
        if(currentTurn >= activeBattlers.Count)
        {
            currentTurn = 0;
        }
        turnWaiting = true;
        UpdateBattle();
        UpdateUIStats();
    }

    public void UpdateBattle()
    {
        bool allEnemiesDead = true;
        bool allPlayersDead = true;

        for(int i = 0; i < activeBattlers.Count; i++)
        {
            if (activeBattlers[i].currentHP < 0)
            {
                activeBattlers[i].currentHP = 0;
            }
            if(activeBattlers[i].currentHP == 0)
            {

            }
            else
            {
                if (activeBattlers[i].isPlayer)
                {
                    allPlayersDead = false;
                }
                else
                {
                    allEnemiesDead = false;
                }
            }
        }
        if(allEnemiesDead || allPlayersDead)
        {
            if (allEnemiesDead)
            {

            }
            else
            {

            }
            battleScene.SetActive(false);
            GameManager.instance.battleActive = false;
            battleActive = false;
        }
        else
        {
            while(activeBattlers[currentTurn].currentHP == 0)
            {
                currentTurn++;
                if(currentTurn >= activeBattlers.Count)
                {
                    currentTurn = 0;
                }
            }
        }
    }

    public IEnumerator EnemyMoveCo()
    {
        turnWaiting = false;
        yield return new WaitForSeconds(1f);
        EnemyAttack();
        yield return new WaitForSeconds(1f);
        NextTurn();
    }

    public void EnemyAttack()
    {
        List<int> players = new List<int>();
        for(int i = 0; i < activeBattlers.Count; i++)
        {
            if(activeBattlers[i].isPlayer && activeBattlers[i].currentHP > 0)
            {
                players.Add(i);
            }
        }
        int selectedTarget = players[Random.Range(0, players.Count)];

        int selectAttack = Random.Range(0, activeBattlers[currentTurn].movesAviable.Length);
        int movePower = 0;
        for(int j = 0; j < moveList.Length; j++)
        {
            if (moveList[j].moveName == activeBattlers[currentTurn].movesAviable[selectAttack])
            {
                Instantiate(moveList[j].theEffect, activeBattlers[selectedTarget].transform.position, activeBattlers[selectedTarget].transform.rotation);
                movePower = moveList[j].movePower;
            }
        }
        Instantiate(enemyAtteckEffect, activeBattlers[currentTurn].transform.position, activeBattlers[currentTurn].transform.rotation);
        DealDamage(selectedTarget, movePower);
    }

    public void DealDamage(int target, int movePower)
    {
        float attackPwr = activeBattlers[currentTurn].strength + activeBattlers[currentTurn].wpnPower;
        float defPwr = activeBattlers[target].defence + activeBattlers[target].armrPower;

        float damageCalc = (attackPwr / defPwr) * movePower * Random.Range(.9f, 1.1f);
        int damageToGive = Mathf.RoundToInt(damageCalc);

        Debug.Log(activeBattlers[currentTurn].charName + " is dealing " + damageCalc + "(" + damageToGive + ") damage to " + activeBattlers[target].charName);
        activeBattlers[target].currentHP -= damageToGive;
        Instantiate(theDamageNumber, activeBattlers[target].transform.position, activeBattlers[target].transform.rotation).SetDamage(damageToGive);
    }

    public void UpdateUIStats()
    {
        for(int i = 0; i < playerNames.Length; i++)
        {
            if (activeBattlers.Count > i && activeBattlers[i].isPlayer)
            {
                BattleChar playerData = activeBattlers[i];
                playerNames[i].gameObject.SetActive(true);
                playerNames[i].text = playerData.charName;
                playerHP[i].text = Mathf.Clamp(playerData.currentHP, 0, int.MaxValue) + "/" + playerData.maxHP;
                playerMP[i].text = Mathf.Clamp(playerData.currentMP, 0, int.MaxValue) + "/" + playerData.maxMP;
            }
            else
            {
                playerNames[i].gameObject.SetActive(false);
            }
        }
    }

    public void PlayerAttack(string moveName, int selectedTarget)
    {
        int movePower = 0;
        for (int j = 0; j < moveList.Length; j++)
        {
            if (moveList[j].moveName == moveName)
            {
                Instantiate(moveList[j].theEffect, activeBattlers[selectedTarget].transform.position, activeBattlers[selectedTarget].transform.rotation);
                movePower = moveList[j].movePower;
            }
        }
        Instantiate(enemyAtteckEffect, activeBattlers[currentTurn].transform.position, activeBattlers[currentTurn].transform.rotation);
        DealDamage(selectedTarget, movePower);
        uiButtonsHolder.SetActive(false);
        targetMenu.SetActive(false);
        NextTurn();
    }

    public void OpenTargetMenu(string moveName)
    {
        targetMenu.SetActive(true);
        List<int> enemies = new List<int>();
        for(int i = 0; i < activeBattlers.Count; i++)
        {
            if (!activeBattlers[i].isPlayer)
            {
                enemies.Add(i);
            }
        }
        for(int j = 0; j < targetButtons.Length; j++)
        {
            if(enemies.Count > j)
            {
                targetButtons[j].gameObject.SetActive(true);
                targetButtons[j].moveName = moveName;
                targetButtons[j].activeBattleTarget = enemies[j];
                targetButtons[j].targetName.text = activeBattlers[enemies[j]].charName;
            }
            else
            {
                targetButtons[j].gameObject.SetActive(false);
            }
        }
    }

    public void OpenMagicMenu()
    {
        magicMenu.SetActive(true);
        for(int i = 0; i < magicButtons.Length; i++)
        {
            if(activeBattlers[currentTurn].movesAviable.Length > i)
            {
                magicButtons[i].gameObject.SetActive(true);
                magicButtons[i].spellName = activeBattlers[currentTurn].movesAviable[i];
                magicButtons[i].nameText.text = magicButtons[i].spellName;
                for(int j = 0; j < moveList.Length; j++)
                {
                    if(moveList[j].moveName == magicButtons[i].spellName)
                    {
                        magicButtons[i].spellCost = moveList[j].moveCost;
                        magicButtons[i].costText.text = magicButtons[i].spellCost.ToString();
                    }
                }
            }
            else
            {
                magicButtons[i].gameObject.SetActive(false);
            }
        }
    }
}