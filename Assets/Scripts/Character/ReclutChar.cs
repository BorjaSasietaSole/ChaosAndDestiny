using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReclutChar : MonoBehaviour
{
    public string questToCompleteOn;

    public CharStats characterToReclut;
    public bool isActive;
    public bool isInteractuate;

    private static bool isHidden = true;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance != null)
        {
            for (int i = 0; i < GameManager.instance.playerStats.Length; i++)
            {
                if (GameManager.instance.playerStats[i].charName == characterToReclut.charName && GameManager.instance.playerStats[i].gameObject.activeSelf)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && isInteractuate && Input.GetButtonDown("Fire1"))
        {
            GameManager.instance.pushCharToArr(characterToReclut);
            GameMenu.instance.UpdateMainStats();
            isActive = false;
            QuestsManager.instance.MarkQuestComplete(questToCompleteOn);
        }
        if (!isActive)
        {
            if (QuestsManager.instance.CheckIfComplete(questToCompleteOn))
            {
                isHidden = false;
                gameObject.SetActive(isHidden);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && isActive)
        {
            isInteractuate = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && isActive)
        {
            isInteractuate = true;
        }
    }
}
