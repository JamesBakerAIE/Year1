﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
public class PickupArea : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == player.keycardLayerMask && player.walkingState.interact == true)
        {

        }
    }
}
