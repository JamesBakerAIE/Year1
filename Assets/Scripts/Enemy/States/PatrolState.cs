using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

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


        Transform currentWayPoint;

        SeenTransition seenTransition;

        public bool seenPlayer = false;
        private void Start()
        {
            seenTransition = GameObject.FindObjectOfType<SeenTransition>();
            transitions.Add(seenTransition);
        }

        public override void Enter()
        {
            Debug.Log("Entered patrol state");
        }
        // Update is called once per frame
        public override Vector3 LogicUpdate(Vector3 enemyPosition)
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


                //return currentWayPoint.position;

            }

            Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

            foreach(Transition transition in transitions)
            {
                transition.CheckTransition(enemyPosition, playerPosition);
            }


            //return Vector3.zero;
            return currentWayPoint.position;
        }
    }
}
