using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLanguage : MonoBehaviour
{
    public void ChangeLanguage_(string language)
    {
        JsonController.ObtenerIdioma(language);
    }
}
