using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReclutChar : MonoBehaviour
{
    public string questToCompleteOn;

    public CharStats characterToReclut;
    public bool isActive;
    public bool isInteractuate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && isInteractuate && Input.GetButtonDown("Fire1"))
        {
            GameManager.instance.pushCharToArr(characterToReclut);
            GameMenu.instance.pushCharToArr(characterToReclut.gameObject);
            GameMenu.instance.UpdateMainStats();
            isActive = false;
            QuestsManager.instance.MarkQuestComplete(questToCompleteOn);
        }
        if (!isActive)
        {
            if (QuestsManager.instance.CheckIfComplete(questToCompleteOn))
            {
                gameObject.SetActive(false);
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
