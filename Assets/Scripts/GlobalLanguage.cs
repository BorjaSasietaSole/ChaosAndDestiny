using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GlobalLanguage : MonoBehaviour
{
    public string actualTextKey;

    private void Awake()
    {
       Text text = GetComponent<Text>();
       text.text = JsonController.GetValueText(actualTextKey);
    }
    private void Update()
    {
        Text text = GetComponent<Text>();
        text.text = JsonController.GetValueText(actualTextKey);
    }
}
