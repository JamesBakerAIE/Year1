using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace EnemyAI
{

    public class PatrolState : State
    {
        public List<Transform> wayPoints;
        public List<Transform> checkedWayPoints;

        public GameObject room;
        GameObject[] doors;
        //GameObject[10] waypoints;
        int selectedDoor;
        public Transform targetWayPoint;
        public Room roomScript;

        public Material selectedWayPointMaterial;


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

        float closestWaypoint = 10000;
        public WayPoint selectedWayPoint;
        // Update is called once per frame
        public override Vector3 LogicUpdate(Vector3 enemyPosition)
        {
            RaycastHit hit;
            if (Physics.Raycast(enemyPosition, -Vector3.up * 1000, out hit, Mathf.Infinity))
            {
 
                room = hit.collider.gameObject;
                room = room.transform.parent.gameObject;

                //room = room.transform.parent.parent.gameObject;
                roomScript = room.GetComponent<Room>();

                if (selectedWayPoint == null)
                    CheckPath();

                closestWaypoint = Vector3.Distance(selectedWayPoint.GetComponent<Transform>().position, enemyPosition);

                if (closestWaypoint < 1)
                {
                    selectedWayPoint.patrolled = true;
                    closestWaypoint = 100000f;
                    CheckPath();
                }

                //return currentWayPoint.position;

            }
            //return Vector3.zero;
            if (selectedWayPoint == null)
                return Vector3.zero;

            selectedWayPoint.gameObject.GetComponent<Renderer>().material = selectedWayPointMaterial;
            return selectedWayPoint.GetComponent<Transform>().position;
        }

        void CheckPath()
        {
            foreach (Transform wayPointTransform in roomScript.wayPoints)
            {

                float wayPointDistance = Vector3.Distance(wayPointTransform.position, this.transform.position);
                WayPoint wayPoint = wayPointTransform.GetComponent<WayPoint>();

                if(wayPoint == selectedWayPoint)
                {
                    wayPoint.patrolled = true;
                }

                if (wayPoint.patrolled == true)
                {
                    StartCoroutine(PatrolledDelay(wayPoint));

                }

                if (wayPointDistance < closestWaypoint && wayPoint.patrolled == false)
                {
                    float check = Random.Range(0, 100);
                    float testCheck = wayPoint.wayPointChance;
                    if (check <= testCheck && wayPoint.patrolled == false)
                    {
                        selectedWayPoint = wayPoint;
                        closestWaypoint = wayPointDistance;
                    }

                }

            }
        }

        IEnumerator PatrolledDelay(WayPoint wayPoint)
        {
            Debug.Log("Starting delay");
            yield return new WaitForSeconds(1f);
            foreach (Transform childWayPoint in wayPoint.transform)
            {
                childWayPoint.GetComponent<WayPoint>().patrolled = true;
                childWayPoint.gameObject.GetComponent<Renderer>().material = selectedWayPointMaterial;
            }
            Debug.Log("Ending delay");

        }
    }
}
