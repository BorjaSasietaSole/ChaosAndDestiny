using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEssentialLoader : MonoBehaviour
{
    public GameObject UIScreen;
    public GameObject player;
    public GameObject gameMan;
    public GameObject auduioMan;
    public GameObject battleManager;

    // Start is called before the first frame update
    void Start()
    {
        if(UIFade.instance == null)
        {
            Instantiate(UIScreen);
        }
        if(PlayerController.instance == null)
        {
            PlayerController clone = Instantiate(player).GetComponent<PlayerController>();
            PlayerController.instance = clone;
        }
        if(GameManager.instance == null)
        {
            Instantiate(gameMan);
        }
        if(AudioManager.instance == null)
        {
            Instantiate(auduioMan);
        }
        if(BattleManager.instance == null)
        {
            Instantiate(battleManager);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
