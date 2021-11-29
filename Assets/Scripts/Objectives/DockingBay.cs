﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers.UIManager;

public class DockingBay : MonoBehaviour
{
    private UIManager uiManager;


    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            uiManager.DockingBayExitFound();
        }
    }
}
