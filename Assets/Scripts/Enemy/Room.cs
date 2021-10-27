using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] List<Transform> hidingSpots;
    [SerializeField] public List<Transform> wayPoints;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform gameObjectParent in this.gameObject.transform.GetComponentInChildren<Transform>())
        {
            //finds all hideySpots in the room
            if (gameObjectParent.gameObject.tag == "HideySpot")
                foreach (Transform hideySpot in gameObjectParent.GetComponentInChildren<Transform>())
                    hidingSpots.Add(hideySpot);
            //finds all waypoints in the room
            if (gameObjectParent.gameObject.tag == "WayPoint")
                foreach (Transform wayPoint in gameObjectParent.GetComponentInChildren<Transform>())
                    wayPoints.Add(wayPoint);

        }
    }
}
