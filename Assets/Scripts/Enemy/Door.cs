using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] public Transform door1;
    [SerializeField] public Transform door2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WayPoint")
            if (door1 == null)
                door1 = other.transform;
            else
                door2 = other.transform;
    }
}
