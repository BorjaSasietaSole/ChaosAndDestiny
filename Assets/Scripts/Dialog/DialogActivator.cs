using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    public string[] lines;

    private bool canActivate = false;

    public bool shouldActivateQuest;
    public bool markComplete;
    public string questToMark;

    private static bool showing = false;
    public GameObject toShow;

    // Start is called before the first frame update
    void Start()
    {
        if (toShow != null)
        {
            toShow.SetActive(showing);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(canActivate && Input.GetButtonDown("Fire1") && !DialogController.instance.dialogBox.activeInHierarchy)
        {
            DialogController.instance.ShowDialog(lines);
            DialogController.instance.ShouldActivateQuestAtEnd(questToMark, markComplete);
            if (toShow != null) 
            {
                showing = true;
                toShow.SetActive(showing);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player") 
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canActivate = false;
        }
    }
}
