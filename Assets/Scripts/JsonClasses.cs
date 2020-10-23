using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JsonContainer
{
    public List<JsonText> texts;

    public string GetText(string key)
    {
        string ret = "";
        foreach (JsonText jst in texts)
        {
            if (jst.key == key)
            {
                return jst.value;
            }
        }
        return ret;
    }
}

[System.Serializable]
public class JsonText
{
    public string key;
    public string value;
}
