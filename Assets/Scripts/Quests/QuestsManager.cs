using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsManager : MonoBehaviour
{
    public string[] questMarkerNames;
    public bool[] questMarkesComplete;

    public static QuestsManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        questMarkesComplete = new bool[questMarkerNames.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetQuestNumber(string questToFind)
    {
        int ret = 0;
        for(int i = 0; i < questMarkerNames.Length; i++)
        {
            if(questMarkerNames[i] == questToFind)
            {
                ret = i;
            }
        }
        if(ret == 0)
        {
            Debug.LogError("Quest " + questToFind + "does not exist");
        }
        return ret;
    }

    public bool CheckIfComplete(string questToCheck)
    {
        bool ret = false;
        if(GetQuestNumber(questToCheck) != 0)
        {
            ret = questMarkesComplete[GetQuestNumber(questToCheck)];
        }
        return ret;
    }

    public void MarkQuestComplete(string questToMark)
    {
        questMarkesComplete[GetQuestNumber(questToMark)] = true;
        UpdateLocalQuestObjects();
    }

    public void MarkQuestIncomplete(string questToMark)
    {
        questMarkesComplete[GetQuestNumber(questToMark)] = false;
        UpdateLocalQuestObjects();
    }

    public void UpdateLocalQuestObjects()
    {
        QuestObjectActivated[] questObjects = FindObjectsOfType<QuestObjectActivated>();

        if(questObjects.Length > 0)
        {
            for(int i = 0; i <questObjects.Length; i++)
            {
                questObjects[i].CheckCompletion();
            }
        }
    }

    public void SaveData()
    {
        for(int i = 0; i < questMarkerNames.Length; i++)
        {
            if (questMarkesComplete[i])
            {
                PlayerPrefs.SetInt("QuestMarker_" + questMarkerNames[i], 1);
            }
            else
            {
                PlayerPrefs.SetInt("QuestMarker_" + questMarkerNames[i], 0);
            }
        }
    }

    public void LoadData()
    {
        for(int i = 0; i < questMarkerNames.Length; i++)
        {
            int valueToSet = 0;
            if(PlayerPrefs.HasKey("QuestMarker_" + questMarkerNames[i]))
            {
                valueToSet = PlayerPrefs.GetInt("QuestMarker_" + questMarkerNames[i]);
            }
            if(valueToSet == 0)
            {
                questMarkesComplete[i] = false;
            }
            else
            {
                questMarkesComplete[i] = true;
            }
        }
    }
}
