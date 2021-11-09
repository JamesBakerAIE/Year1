using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace EnemyAI
{
    public class SearchState : State
    {

        public GameObject room;
        public Room roomScript;
        // Start is called before the first frame update
        Transform playerPosition;
        TimerTransition timerTransition;

        public Material selectedHidingSpotMaterial;
        public Material hdingSpotMaterial;
        public override void Enter()
        {
            //Debug.Log("Entered agitated state");
            //playersLastPosition = GameObject.FindObjectOfType<PlayerController>().transform.position;
            //timerTransition = GameObject.FindObjectOfType<TimerTransition>();
            //transitions.Add(timerTransition);
            Debug.Log("Entered agitated state");

        }

        private void Start()
        {
            playerPosition = GameObject.FindObjectOfType<PlayerController>().transform;
            timerTransition = GameObject.FindObjectOfType<TimerTransition>();
            transitions.Add(timerTransition);
        }



        float closestHidingSpot = 10000;
        public HideSpot selectedHidingSpot;

        public override Vector3 LogicUpdate(Vector3 enemyPosition)
        {
            RaycastHit hit;
            if (Physics.Raycast(enemyPosition, -Vector3.up * 1000, out hit, Mathf.Infinity))
            {

                if (room == hit.collider.gameObject.transform.parent.gameObject)
                {
                    //Debug.Log("Already in this room");
                }
                else
                {
                    room = hit.collider.gameObject.transform.parent.gameObject;
                    roomScript = room.GetComponent<Room>();
                }
            }

            if (closestHidingSpot < 3)
            {
                selectedHidingSpot.searched = true;
                closestHidingSpot = 100000f;
                selectedHidingSpot.gameObject.GetComponent<Renderer>().material = selectedHidingSpotMaterial;
                CheckHidingSpots();
            }

            CheckHidingSpots();

            return selectedHidingSpot.GetComponent<Transform>().position;
        }

        public void CheckHidingSpots()
        {
            foreach (Transform hideSpotTransform in roomScript.hidingSpots)
            {

                float hideSpotDistance = Vector3.Distance(hideSpotTransform.position, this.transform.position);
                HideSpot hideSpot = hideSpotTransform.GetComponent<HideSpot>();


                if (hideSpot.searched == true)
                {
                    //if (hideSpot.searchChance == 100)
                    //{
                    //    requiredWayPointsVisited++;
                    //}
                    //StartCoroutine(PatrolledDelay(wayPoint));

                }

                if (hideSpotDistance < closestHidingSpot && hideSpot.searched == false)
                {
                    float check = Random.Range(0, 100);
                    float testCheck = hideSpot.searchChance;
                    if (check <= testCheck && hideSpot.searched == false)
                    {
                        selectedHidingSpot = hideSpot;
                        closestHidingSpot = hideSpotDistance;
                    }


                }

            }

            //NEED TO SET ALL HIDING SPOTS TO FALSE AS SEARCHED ONCE THE SEARCH STATE HAS ENDED
                //foreach (Transform wayPoint in roomScript.wayPoints)
                //{
                //    wayPoint.GetComponent<WayPoint>().patrolled = false;
                //    wayPoint.GetComponent<Renderer>().material = wayPointMaterial;
                //    foreach (Transform wayPointChild in wayPoint.GetComponentInChildren<Transform>())
                //    {
                //        wayPointChild.GetComponent<WayPoint>().patrolled = false;
                //        wayPointChild.GetComponent<Renderer>().material = wayPointMaterial;
                //    }
                //}
        }
    }
}
