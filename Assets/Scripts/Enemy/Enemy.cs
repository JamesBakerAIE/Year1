using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyAI
{

    public class Enemy : MonoBehaviour
    {
        //adding using the 'new' keyword means that it doesn't call the start functio
        PatrolState patrolState;
        ChaseState chaseState;
        AttackState attackState;
        State currentState;
        GameObject room;

        public NavMeshAgent enemyAgent;
        LayerMask ignoreLayer;
        // Start is called before the first frame update
        void Start()
        {
            patrolState = gameObject.AddComponent<PatrolState>();
            chaseState = gameObject.AddComponent<ChaseState>();
            attackState = gameObject.AddComponent<AttackState>();
            currentState = patrolState;
            ignoreLayer = Physics.IgnoreRaycastLayer;
        }

        // Update is called once per frame
        Vector3 direction;

        Transform playerPosition;
        void Update()
        {
            direction = (this.transform.position - playerPosition.position);
            //Debug.DrawRay(this.transform.position, -direction, Color.red, Mathf.Infinity);

            //Debug.DrawRay(this.transform.position, -direction, Color.red, Mathf.Infinity);

            enemyAgent.destination = currentState.UpdateAgent(this.transform.position);
        }

        public Vector3 eyesOffset;

        public void SeenPlayer(Transform playerPosition)
        {
            if (currentState == attackState)
                return;

            this.playerPosition = playerPosition;

            RaycastHit hit;
            Debug.DrawRay(this.transform.position + eyesOffset, -direction, Color.red, 1f);

            Ray ray = new Ray(this.transform.position + eyesOffset, -direction);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreLayer))
            {
                //Debug.DrawRay(this.transform.position, Vector3.forward, Color.red);
                if (hit.collider.tag == "Player")
                {
                    currentState = chaseState;
                }
                else
                {
                    currentState = patrolState;
                }
            }
        }

        public void AttackPlayer()
        {
            currentState = attackState;
            Debug.Log("Attacking player");
        }

    }
}
