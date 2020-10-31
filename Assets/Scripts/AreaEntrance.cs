using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    public string AreaTransitionName;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.instance && AreaTransitionName.Equals(PlayerController.instance.areaTransitionName))
        {
            PlayerController.instance.transform.position = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
