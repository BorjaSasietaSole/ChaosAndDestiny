using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSavedGame : MonoBehaviour
{
    public float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if(currentTime <= 0)
            {
                SceneManager.LoadScene(PlayerPrefs.GetString("Current_Scene"));
                GameManager.instance.LoadData();
                QuestsManager.instance.LoadData();
            }
        }
    }
}
