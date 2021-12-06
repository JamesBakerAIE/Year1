using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace EnemyAI
{

    public class PatrolState : State
    {
        //Waypoints
        [SerializeField] List<Transform> wayPoints;
        [SerializeField] List<Transform> checkedWayPoints;
        WayPoint selectedWayPoint;
        Transform targetWayPoint;
        //Starts at a large number so that the closest waypoint is accurate
        float closestWaypoint = 10000;

        Room roomScript;
        GameObject room;

        //For Debugging
        public Material selectedWayPointMaterial;
        public Material wayPointMaterial;


        public bool seenPlayer = false;
        private void Start()
        {
            GetComponents();

            transitions.Add(seenTransition);

            //hearingCollider = GetComponentInChildren<SphereCollider>();

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
            if (enemyAudio.clip != enemySound)
            {
                enemyAudio.clip = enemySound;
                enemyAudio.Play();
                enemyAudio.loop = true;
            }
            isRunning = false;
            //hearingCollider.radius = hearingRadius;
        }
        public override void Exit()
        {

        }

        public override float GetSpeed()
        {
            return movementSpeed;
        }

        // Update is called once per frame
        public override Vector3 DestinationUpdate(Vector3 enemyPosition)
        {
            //Gets the room and all the waypoints and hiding spots by shooting a ray at the floor and getting the gameobject that contains the room script
            RaycastHit hit;
            if (Physics.Raycast(enemyPosition, -Vector3.up * 1000, out hit, Mathf.Infinity))
            {
                
                if (room == hit.collider.gameObject.transform.parent.parent.gameObject)
                {
                    //Debug.Log("Already in this room");
                }
                else
                {
                    room = hit.collider.gameObject.transform.parent.parent.gameObject;
                    roomScript = room.GetComponent<Room>();
                }
            }

            if (selectedWayPoint == null)
                CheckPath();

            closestWaypoint = Vector3.Distance(selectedWayPoint.GetComponent<Transform>().position, enemyPosition);

            if (closestWaypoint < 3)
            {
                selectedWayPoint.patrolled = true;
                closestWaypoint = 100000f;
                CheckPath();
            }

            //return Vector3.zero;
            if (selectedWayPoint == null)
                return Vector3.zero;

            selectedWayPoint.gameObject.GetComponent<Renderer>().material = selectedWayPointMaterial;
            return selectedWayPoint.GetComponent<Transform>().position;
        }


        public void CheckPath()
        {
            //all waypoints that are required to be visited before resetting the waypoints
            int requiredWayPoints = 0;
            int requiredWayPointsVisited = 0;

            foreach (Transform wayPointTransform in roomScript.wayPoints)
            {

                float wayPointDistance = Vector3.Distance(wayPointTransform.position, this.transform.position);
                WayPoint wayPoint = wayPointTransform.GetComponent<WayPoint>();

                if (wayPoint.wayPointChance == 100)
                    requiredWayPoints++;

                if (wayPoint.patrolled == true)
                {
                    if (wayPoint.wayPointChance == 100)
                    {
                        requiredWayPointsVisited++;
                    }
                    //StartCoroutine(PatrolledDelay(wayPoint));

                }

                if (wayPointDistance < closestWaypoint && wayPoint.patrolled == false)
                {
                    //Calculate whether or not to patrol the waypoint based off it's patrol chance
                    float check = Random.Range(0, 100);
                    float testCheck = wayPoint.wayPointChance;
                    if (check <= testCheck && wayPoint.patrolled == false)
                    {
                        selectedWayPoint = wayPoint;
                        closestWaypoint = wayPointDistance;
                    }

                }

            }
            //Reset the waypoints when all required waypoints have been pattolled
            if (requiredWayPoints == requiredWayPointsVisited)
                foreach (Transform wayPoint in roomScript.wayPoints)
                {
                    wayPoint.GetComponent<WayPoint>().patrolled = false;
                    //for debugging
                    wayPoint.GetComponent<Renderer>().material = wayPointMaterial;
                    //Code for when waypoints had children, reset them as well
                    //foreach (Transform wayPointChild in wayPoint.GetComponentInChildren<Transform>())
                    //{
                    //    wayPointChild.GetComponent<WayPoint>().patrolled = false;
                    //    wayPointChild.GetComponent<Renderer>().material = wayPointMaterial;
                    //}
                }
        }

        //Sets the waypoint as patrolled after a delay (Delay no longer needed)
        //IEnumerator PatrolledDelay(WayPoint wayPoint)
        //{
        //    Debug.Log("Starting delay");
        //    yield return new WaitForSeconds(1f);
        //    foreach (Transform childWayPoint in wayPoint.transform)
        //    {
        //        childWayPoint.GetComponent<WayPoint>().patrolled = true;
        //        childWayPoint.gameObject.GetComponent<Renderer>().material = selectedWayPointMaterial;
        //    }
        //    Debug.Log("Ending delay");

        //}
    }
}
