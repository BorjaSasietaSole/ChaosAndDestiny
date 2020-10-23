using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonController : MonoBehaviour
{
    private static JsonContainer texts;
    private const SystemLanguage defaultLanguage = SystemLanguage.English;

    public static string GetValueText(string textoId)
    {
        string RetText = "Not translate";

        if (texts == null)
        {
            ObtenerIdioma(Application.systemLanguage.ToString());
        }
        RetText = texts.GetText(textoId);

        return RetText;
    }

    public static void ObtenerIdioma(string language)
    {
        switch (language)
        {
            case "Spanish":
                LoadJsonFile(SystemLanguage.Spanish.ToString());
                break;
            case "Catalan":
                LoadJsonFile(SystemLanguage.Catalan.ToString());
                break;
            default:
                LoadJsonFile(SystemLanguage.English.ToString());
                break;
        }
    }

    private static void LoadJsonFile(string name)
    {
        TextAsset asset = Resources.Load<TextAsset>(name);
        if (asset != null)
        {
            texts = JsonUtility.FromJson<JsonContainer>(asset.text);
        }
        else
        {
            Debug.LogWarning("No s'ha trobat el fitxer " + name);
        }

    }
}
