using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    GameObject room;
    GameObject[] doors;
    GameObject[10] waypoints;
    int selectedDoor;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("patrol state selected");
    }

    // Update is called once per frame
    public override Vector3 UpdateAgent(Vector3 enemyPosition)
    {
        Debug.Log("patrolling");
        RaycastHit hit;
        if(Physics.Raycast(enemyPosition, -Vector3.up * 1000, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(enemyPosition, -Vector3.up * 1000, Color.red);
            Debug.Log("hiting floor");
            Debug.Log(hit.collider.gameObject.name);
            room = hit.collider.gameObject;
            room = room.transform.parent.parent.gameObject;
            int count = 0;
            foreach (GameObject waypoint in room.gameObject.transform.parent.parent.GetComponentInChildren<Transform>().parent)
            {
                if (waypoint.layer == 10{
                    waypoints[count];
                    count++;
                }
            }
        }
        return Vector3.zero;
    }
}
