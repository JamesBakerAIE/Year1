using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<HideySpot> hidingSpots;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform hideySpotParent in this.gameObject.transform.GetComponentInChildren<Transform>())
        {
            /*
            foreach(HideySpot hideySpot in hideySpotParent.GetComponent<HideySpot>())
            {
                hidingSpots.Add(hideySpot);
            }
            */
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
