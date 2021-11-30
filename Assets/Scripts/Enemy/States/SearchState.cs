﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace EnemyAI
{
    public class SearchState : State
    {
        public float distanceFromLocker;
        GameObject room;
        Room roomScript;
        // Start is called before the first frame update
        Transform playerPosition;
        SeenTransition seenTransition;
        LockerTransition lockerTransition;

        public Material selectedHidingSpotMaterial;
        public Material hdingSpotMaterial;

        public float maxDistanceFromHideSpot;

        public List<Transform> hidingSpotsToSearch;

        Vector3 lockerDestination;

        public bool foundPlayer;

        GameObject attackRange;

        public bool hasSniffed = false;


        public float timeAgitated = 5;
        public float timeElapsed = 0;

        Vector3 lockerRotation = Vector3.zero;
        public Vector3 lockerOffset;

        private void Start()
        {
            playerPosition = GameObject.FindObjectOfType<PlayerController>().transform;

            seenTransition = GameObject.FindObjectOfType<SeenTransition>();
            lockerTransition = GameObject.FindObjectOfType<LockerTransition>();
            //hearingCollider = GetComponentInChildren<SphereCollider>();
            transitions.Add(seenTransition);
            transitions.Add(lockerTransition);
            attackRange = FindObjectOfType<AttackRange>().gameObject;
        }
        public override void Enter()
        {
            attackRange.SetActive(false);
            RaycastHit hit;
            //Gets hiding spots, and resets hiding spots
            if (Physics.Raycast(this.transform.position, -Vector3.up * 1000, out hit, Mathf.Infinity))
            {
                room = hit.collider.gameObject.transform.parent.parent.gameObject;
                roomScript = room.GetComponent<Room>();
                hidingSpotsToSearch.Clear();
                CheckHidingSpots();
            }
            foundPlayer = false;
            if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<AudioSource>().clip != enemySound)
            {
                GameObject.FindGameObjectWithTag("Enemy").GetComponent<AudioSource>().clip = enemySound;
                GameObject.FindGameObjectWithTag("Enemy").GetComponent<AudioSource>().Play();
                GameObject.FindGameObjectWithTag("Enemy").GetComponent<AudioSource>().loop = true;
            }
            isRunning = false;
            //hearingCollider.radius = hearingRadius;


        }

        public override float GetSpeed()
        {
            return movementSpeed;
        }


        public override Vector3 DestinationUpdate(Vector3 enemyPosition)
        {
            //Vector3 tempTransform;
            //if (hidingSpotsToSearch.Count > -1)
            //    tempTransform = hidingSpotsToSearch[0].position + hidingSpotsToSearch[0].forward * distanceFromLocker;
            //once all hiding spots that it wants to search have been searched, change state to patrolling
            if (hidingSpotsToSearch.Count == 0)
            {
                this.GetComponent<Enemy>().stateMachine.ChangeState(this.GetComponent<PatrolState>());
                return this.transform.position;
            }

            Debug.DrawLine(this.transform.position, lockerDestination, Color.cyan);
            Debug.Log(hidingSpotsToSearch[0].gameObject.name);
            //if close to the locker, set location as the next hiding spot
            if (Vector3.Distance(this.transform.position, lockerDestination) < 1)
            {
                isSearching = true;
                //closestHidingSpot = 100000f;
                //hidingSpotsToSearch[0].GetComponentInChildren<HideSpot>().doorObject.material = selectedHidingSpotMaterial;
                if (hidingSpotsToSearch[0].GetComponentInChildren<HideSpot>().hasPlayer == true)
                    foundPlayer = true;

                if (timeElapsed >= timeAgitated)
                {
                    FindObjectOfType<Enemy>().animator.SetBool("Sniffing", false);
                    if (hidingSpotsToSearch[0].GetComponentInChildren<HideSpot>().hasPlayer)
                    {
                        hidingSpotsToSearch[0].GetComponentInChildren<HideSpot>().AnimateAttack();
                    }

                    hidingSpotsToSearch.Remove(hidingSpotsToSearch[0]);
                    Debug.Log("Searched locker");

                    hidingSpotsToSearch.Sort((Transform t1, Transform t2) =>
                    {
                        var dist1 = Vector3.Distance(transform.position, t1.position);
                        var dist2 = Vector3.Distance(transform.position, t2.position);
                        return dist1.CompareTo(dist2);
                    });
                    lockerRotation = Vector3.zero;
                    hasSniffed = true;
                }
                else
                {
                    FindObjectOfType<Enemy>().animator.SetBool("Sniffing", true);
                    timeElapsed += Time.deltaTime;
                    hasSniffed = false;
                    lockerRotation = hidingSpotsToSearch[0].rotation.eulerAngles;
                }

                if (hidingSpotsToSearch.Count > 0)
                {
                    lockerDestination = (hidingSpotsToSearch[0].position + lockerOffset) + hidingSpotsToSearch[0].forward * distanceFromLocker;
                }
            }

            return lockerDestination;
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
                    float testCheck = hideSpotTransform.GetComponentInChildren<HideSpot>().searchChance;
                    //hideSpotTransform.GetComponent<HideSpot>().doorObject.material = hdingSpotMaterial;
                    if (check <= testCheck)
                    {
                        hidingSpotsToSearch.Add(hideSpotTransform);
                    }
                }
            }


            //sorts hidingspots in order of closeness to the enemy
            hidingSpotsToSearch.Sort((Transform t1, Transform t2) =>
            {
                var dist1 = Vector3.Distance(transform.position, t1.position);
                var dist2 = Vector3.Distance(transform.position, t2.position);
                return dist1.CompareTo(dist2);
            });

            lockerDestination = (hidingSpotsToSearch[0].position + lockerOffset) + hidingSpotsToSearch[0].forward * distanceFromLocker;
        }

        public override Transform RotationUpdate()
        {
            if (lockerRotation != Vector3.zero)
            {
                return hidingSpotsToSearch[0];
            }
            else
            {
                return null;
            }
        }

        public override void Exit()
        {
            attackRange.SetActive(true);
        }
    }
}
