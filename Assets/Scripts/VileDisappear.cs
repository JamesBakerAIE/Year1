using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VileDisappear : MonoBehaviour
{
    public GameObject Vile;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Destroy(this.gameObject);
        }
    }
    

}

