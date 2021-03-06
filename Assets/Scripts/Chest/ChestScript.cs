﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public Item terasure;
    public bool isActive;
    public bool isInteractuate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && isInteractuate && Input.GetButtonDown("Fire1"))
        {
            GameManager.instance.AddItem(terasure.name);
            isActive = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && isActive)
        {
            isInteractuate = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && isActive)
        {
            isInteractuate = true;
        }
    }
}
