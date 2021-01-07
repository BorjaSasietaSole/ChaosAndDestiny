using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStarter : MonoBehaviour
{
    public BattleType[] potentialBattles;

    public bool activateOnEnter, activateOnStay, ActivateOnExit;

    private bool inArea;
    public float timeBetweenBattle = 10f;
    private float timeBetweenCounter;

    public bool desactivateAfterStarting, cannotFlee;

    public bool shouldCompleteQuest;
    public string questToComplete;

    // Start is called before the first frame update
    void Start()
    {
        timeBetweenCounter = Random.Range(timeBetweenBattle * 0.5f, timeBetweenBattle * 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (inArea && PlayerController.instance.canMove)
        {
            if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                timeBetweenCounter -= Time.deltaTime;
            }
            if(timeBetweenCounter <= 0)
            {
                timeBetweenCounter = Random.Range(timeBetweenBattle * 0.5f, timeBetweenBattle * 1.5f);
                StartCoroutine(StartBattleCo());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (activateOnEnter)
            {
                StartCoroutine(StartBattleCo());
            }
            else
            {
                inArea = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (ActivateOnExit)
            {
                StartCoroutine(StartBattleCo());
            }
            else
            {
                inArea = false;
            }
        }
    }

    public IEnumerator StartBattleCo()
    {
        UIFade.instance.FadeToBlack();
        GameManager.instance.battleActive = true;

        int startBattle = Random.Range(0, potentialBattles.Length);

        BattleManager.instance.rewardItems = potentialBattles[startBattle].rewardItems;
        BattleManager.instance.rewardXP = potentialBattles[startBattle].rewardExp;

        yield return new WaitForSeconds(1.5f);

        BattleManager.instance.BattleStart(potentialBattles[startBattle].enemies, cannotFlee);
        UIFade.instance.FadeFromBlack();
        if (desactivateAfterStarting)
        {
            gameObject.SetActive(false);
        }
        BattleReward.instance.markQuestComplete = shouldCompleteQuest;
        BattleReward.instance.questToMark = questToComplete;
    }
}
