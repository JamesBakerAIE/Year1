using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{

    public class PatrolState : State
    {
        public GameObject room;
        GameObject[] doors;
        //GameObject[10] waypoints;
        int selectedDoor;
        public Transform targetWayPoint;
        public Room roomScript;
        // Start is called before the first frame update
        void Start()
        {
            //Debug.Log("patrol state selected");
        }

        Transform currentWayPoint;

        // Update is called once per frame
        public override Vector3 UpdateAgent(Vector3 enemyPosition)
        {
            RaycastHit hit;

            if (Physics.Raycast(enemyPosition, -Vector3.up * 1000, out hit, Mathf.Infinity))
            {
 
                room = hit.collider.gameObject;
                room = room.transform.parent.parent.gameObject;
                roomScript = room.GetComponent<Room>();

                if (currentWayPoint == null)
                {
                    currentWayPoint = roomScript.wayPoints[0];
                }

                foreach (Transform wayPoint in roomScript.wayPoints)
                {
                    //INSERT RANDOM WAYPOINT PICKING
                    //float thisDistance = Vector3.Distance(wayPoint.position, enemyPosition);
                    float currentClosest = Vector3.Distance(currentWayPoint.position, enemyPosition);
                    if (currentClosest < 1)
                        currentWayPoint = roomScript.wayPoints[Random.Range(0, roomScript.wayPoints.Count)];
                }


                return currentWayPoint.position;

                //foreach(Transform wayPoint in roomScript.wayPoints)
                //{
                //    targetWayPoint = wayPoint;
                //    return wayPoint.transform.position;
                //}

            }
            return Vector3.zero;
        }
    }
}
