﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace EnemyAI
{
    public class SearchState : State
    {
        public float stateSpeed;
        public float distanceFromLocker;
        public GameObject room;
        public Room roomScript;
        // Start is called before the first frame update
        Transform playerPosition;
        SeenTransition seenTransition;

        public Material selectedHidingSpotMaterial;
        public Material hdingSpotMaterial;

        public float maxDistanceFromHideSpot;

        public List<Transform> hidingSpotsToSearch;

        Vector3 lockerDestination;
        public override void Enter()
        {
            RaycastHit hit;
            //Gets hiding spots, and resets hiding spots
            if (Physics.Raycast(this.transform.position, -Vector3.up * 1000, out hit, Mathf.Infinity))
            {
                room = hit.collider.gameObject.transform.parent.gameObject;
                roomScript = room.GetComponent<Room>();
                hidingSpotsToSearch.Clear();
                CheckHidingSpots();
            }
        }

        private void Start()
        {
            playerPosition = GameObject.FindObjectOfType<PlayerController>().transform;

            seenTransition = GameObject.FindObjectOfType<SeenTransition>();
            transitions.Add(seenTransition);
        }

        public override float GetSpeed()
        {
            return stateSpeed;
        }



        public override Vector3 DestinationUpdate(Vector3 enemyPosition)
        {

            //once all hiding spots that it wants to search have been searched, change state to patrolling
            if (hidingSpotsToSearch.Count == 0)
            {
                this.GetComponent<Enemy>().stateMachine.ChangeState(this.GetComponent<PatrolState>());
                return this.transform.position;
            }

            Debug.DrawLine(this.transform.position, lockerDestination, Color.cyan);

            //if close to the locker, set location as the next hiding spot
            if (Vector3.Distance(this.transform.position, lockerDestination) < 1)
            {
                //closestHidingSpot = 100000f;
                hidingSpotsToSearch[0].GetComponent<HideSpot>().doorObject.material= selectedHidingSpotMaterial;
                hidingSpotsToSearch.Remove(hidingSpotsToSearch[0]);
                Debug.Log("Searched locker");

                hidingSpotsToSearch.Sort((Transform t1, Transform t2) =>
                {
                    var dist1 = Vector3.Distance(transform.position, t1.position);
                    var dist2 = Vector3.Distance(transform.position, t2.position);
                    return dist1.CompareTo(dist2);
                });

                lockerDestination = hidingSpotsToSearch[0].position + hidingSpotsToSearch[0].forward * distanceFromLocker;

            }

            return hidingSpotsToSearch[0].position + hidingSpotsToSearch[0].forward * 2;
        }

        public void CheckHidingSpots()
        {
            foreach (Transform hideSpotTransform in roomScript.hidingSpots)
            {

                float hideSpotDistance = Vector3.Distance(hideSpotTransform.position, this.transform.position);

                if (hideSpotDistance < maxDistanceFromHideSpot)
                {
                    //checks if it should be added to the hidingSpotSearchList
                    float check = Random.Range(0, 100);
                    float testCheck = hideSpotTransform.GetComponent<HideSpot>().searchChance;
                    //hideSpotTransform.GetComponent<HideSpot>().doorObject.material = hdingSpotMaterial;

                    if (check <= testCheck)
                    {
                        hidingSpotsToSearch.Add(hideSpotTransform);
                    }
                }
            }


            //sorts hidingspots in order of closenessv to the enemy
            hidingSpotsToSearch.Sort((Transform t1, Transform t2) =>
            {
                var dist1 = Vector3.Distance(transform.position, t1.position);
                var dist2 = Vector3.Distance(transform.position, t2.position);
                return dist1.CompareTo(dist2);
            });

            lockerDestination = hidingSpotsToSearch[0].position + hidingSpotsToSearch[0].forward * distanceFromLocker;
        }
    }
}
