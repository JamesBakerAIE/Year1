﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class displayControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GetComponent<TextMesh>().text = clickControl.playerCode;
    }
}
