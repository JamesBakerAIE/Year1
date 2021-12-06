using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace EnemyAI
{
    public class SearchState : State
    {
        [Tooltip("Distance Offset From The Locker Door The Enemy Has to Be For Enemy To Search")]
        public float distanceFromLocker;

        //Room
        GameObject room;
        Room roomScript;
        // Start is called before the first frame update
        [Tooltip("Radius The Enemy Will Search Lockers")]
        public float maxDistanceFromHideSpot;

        [Tooltip("List Of Hiding Spots The Enemy Is Going To Search")]
        [SerializeField] protected List<Transform> hidingSpotsToSearch;

        //Position the enemy goes to to search the locker
        Vector3 lockerDestination;

        public bool foundPlayer;

        public bool hasSniffed = false;


        bool inLockerVicinity = false;
        public float timeSearching = 5;
        public float timeElapsed = 0;

        Vector3 lockerRotation = Vector3.zero;
        public Vector3 lockerOffset;

        public Transform foundPlayerLocker = null;

        private void Start()
        {
            GetComponents();
            //hearingCollider = GetComponentInChildren<SphereCollider>();
            transitions.Add(seenTransition);
            transitions.Add(lockerTransition);
        }

        void GetComponents()
        {
            enemyAudio = GetComponent<AudioSource>();
            playerPosition = GameObject.FindObjectOfType<PlayerController>().transform;

            enemyStateMachine = FindObjectOfType<StateMachine>();


            patrolState = GetComponent<PatrolState>();
            searchState = GetComponent<SearchState>();
            agitatedState = GetComponent<AgitatedState>();
            chaseState = GetComponent<ChaseState>();
            attackState = GetComponent<AttackState>();

            //Transitions
            seenTransition = GetComponent<SeenTransition>();
            lockerTransition = GetComponent<LockerTransition>();
            timerTransition = GetComponent<TimerTransition>();

            attackRange = FindObjectOfType<AttackRange>().gameObject;
        }

        public override void Enter()
        {
            attackRange.SetActive(false);
            RaycastHit hit;

            if (foundPlayerLocker != null)
                return;

            //Gets hiding spots, and resets hiding spots to search for this search
            if (Physics.Raycast(this.transform.position, -Vector3.up * 1000, out hit, Mathf.Infinity))
            {
                room = hit.collider.gameObject.transform.parent.parent.gameObject;
                roomScript = room.GetComponent<Room>();
                hidingSpotsToSearch.Clear();
                CheckHidingSpots();
            }
            foundPlayer = false;
            if (enemyAudio.clip != enemySound)
            {
                enemyAudio.clip = enemySound;
                enemyAudio.Play();
                enemyAudio.loop = true;
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
            //if it saw the player enter a locker, search that locker
            if(foundPlayerLocker != null)
            {
                lockerDestination = (foundPlayerLocker.position + lockerOffset) + foundPlayerLocker.forward * distanceFromLocker;


                Debug.DrawLine(this.transform.position, lockerDestination, Color.cyan, 100);
                Debug.Log("Found player locker");
            }
            //if no more lockers to search, return to patrolling
            if (hidingSpotsToSearch.Count == 0 && foundPlayerLocker == null)
            {
                enemyStateMachine.ChangeState(patrolState);
                return this.transform.position;
            }

            Debug.DrawLine(this.transform.position, lockerDestination, Color.cyan);
            //Debug.Log(hidingSpotsToSearch[0].gameObject.name);
            //if close to the locker, set location as the next hiding spot
            if (Vector3.Distance(this.transform.position, lockerDestination) < 1)
            {
                isSearching = true;

                if (foundPlayerLocker != null)
                {
                    if (foundPlayerLocker.GetComponentInChildren<HideSpot>().hasPlayer)
                    {
                        foundPlayer = true;
                        foundPlayerLocker.GetComponentInChildren<HideSpot>().AnimateAttack();
                        return lockerDestination;
                    }
                    else
                    {
                        foundPlayerLocker = null;
                        CheckHidingSpots();
                    }
                }
                //if time has run out for searching this locker
                if (timeElapsed >= timeSearching && foundPlayerLocker == null)
                {
                    FindObjectOfType<Enemy>().animator.SetBool("Sniffing", false);
                    if (hidingSpotsToSearch[0].GetComponentInChildren<HideSpot>().hasPlayer)
                    {
                        foundPlayer = true;
                        hidingSpotsToSearch[0].GetComponentInChildren<HideSpot>().AnimateAttack();
                    }

                    hidingSpotsToSearch.Remove(hidingSpotsToSearch[0]);
                    Debug.Log("Searched locker");

                    inLockerVicinity = false;

                    //find next closest locker
                    hidingSpotsToSearch.Sort((Transform t1, Transform t2) =>
                    {
                        var dist1 = Vector3.Distance(transform.position, t1.position);
                        var dist2 = Vector3.Distance(transform.position, t2.position);
                        return dist1.CompareTo(dist2);
                    });

                    lockerRotation = Vector3.zero;
                    hasSniffed = true;
                    timeElapsed = 0;
                }
                else
                {
                    FindObjectOfType<Enemy>().animator.SetBool("Sniffing", true);
                    timeElapsed += Time.deltaTime;
                    hasSniffed = false;
                    inLockerVicinity = true;

                    lockerRotation = hidingSpotsToSearch[0].rotation.eulerAngles;
                }

                if (hidingSpotsToSearch.Count > 0)
                {
                    lockerDestination = (hidingSpotsToSearch[0].position + lockerOffset) + hidingSpotsToSearch[0].forward * distanceFromLocker;
                    //timeElapsed = 0;
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
                    //checks if it should be added to the hidingSpotSearchList depending on the locker's search chance
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

        public override Vector3 RotationUpdate()
        {
            //if (lockerRotation != Vector3.zero)
            //{
            //currently not getting correct rotation for specific lockers
            //look at child with hideyspot
            if (inLockerVicinity)
            {
                Vector3 newRotation = Vector3.RotateTowards(this.transform.position, hidingSpotsToSearch[0].GetComponentInChildren<HideSpot>().transform.position, 10000, 1000);
                return hidingSpotsToSearch[0].GetComponentInChildren<HideSpot>().transform.position;
            }
            else
            {
                return Vector3.zero;
            }
        }

        public override void Exit()
        {
            attackRange.SetActive(true);
        }
    }
}
