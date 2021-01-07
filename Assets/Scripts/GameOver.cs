using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string mainMenuScene;
    public string gameLoadScene;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(PlayerController.instance.gameObject);
        AudioManager.instance.PlayBGM(4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitToMain()
    {
        Destroy(GameManager.instance.gameObject);
        Destroy(GameMenu.instance.gameObject);
        Destroy(AudioManager.instance.gameObject);
        SceneManager.LoadScene(mainMenuScene);
    }
    public void LoadScene()
    {
        Destroy(GameManager.instance.gameObject);
        Destroy(GameMenu.instance.gameObject);
        Destroy(BattleManager.instance.gameObject);
        Destroy(AudioManager.instance.gameObject);
        SceneManager.LoadScene(gameLoadScene);
    }
}
