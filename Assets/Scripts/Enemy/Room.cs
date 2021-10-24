using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Transform> hidingSpots;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform hideySpot in this.gameObject.transform.GetComponentInChildren<Transform>().GetComponentInChildren<HideySpot>().transform)
        {
            hidingSpots.Add(hideySpot);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
