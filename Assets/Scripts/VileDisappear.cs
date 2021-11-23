using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VileDisappear : MonoBehaviour
{
    public GameObject Vile;

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            {
            Vile.SetActive(false);
        }
    }



}

