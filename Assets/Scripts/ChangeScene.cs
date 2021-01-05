using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string SceneToLoad;

    public GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Current_Scene"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadSceneSaved()
    {
        SceneManager.LoadScene("Carregar");
    }

    public void ChangeScene_(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Eixint del joc");
    }
}
