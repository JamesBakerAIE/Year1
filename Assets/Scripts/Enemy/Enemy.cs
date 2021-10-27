using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyAI
{

    public class Enemy : MonoBehaviour
    {
        public StateMachine stateMachine;
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
            stateMachine = gameObject.GetComponent<StateMachine>();
            patrolState = gameObject.GetComponent<PatrolState>();
            chaseState = gameObject.GetComponent<ChaseState>();
            attackState = gameObject.GetComponent<AttackState>();

            //currentState = patrolState;
            stateMachine.Initialize(patrolState);
        }

        // Update is called once per frame
        Vector3 direction;
        public bool seenPlayer = false;
        Transform playerPosition;
        void Update()
        {
            //direction = (this.transform.position - playerPosition.position);
            //Debug.DrawRay(this.transform.position, -direction, Color.red, Mathf.Infinity);
            Debug.Log(stateMachine.CurrentState.ToString());
            //Debug.DrawRay(this.transform.position, -direction, Color.red, Mathf.Infinity);
            enemyAgent.destination = stateMachine.CurrentState.LogicUpdate(this.transform.position);
            //enemyAgent.destination = currentState.LogicUpdate(this.transform.position);
        }

        public Vector3 eyesOffset;
        public Vector3 directionOffset;


        //public void SeenPlayer(Transform playerPosition, bool seen)
        //{
        //    if (seen == false)
        //    {
        //        stateMachine.ChangeState(patrolState);
        //        return;
        //    }
        //    if (stateMachine.CurrentState == attackState)
        //        return;

        //    this.playerPosition = playerPosition;
        //    RaycastHit hit;

        //    Ray ray = new Ray(this.transform.position + eyesOffset, -direction + directionOffset);
        //    if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreLayer))
        //    {
        //        //Debug.DrawRay(this.transform.position, Vector3.forward, Color.red);
        //        if (hit.collider.tag == "Player")
        //        {
        //            if (stateMachine.CurrentState != chaseState)
        //                stateMachine.ChangeState(chaseState);

        //            //Debug.DrawRay(this.transform.position + eyesOffset, -direction + directionOffset, Color.green, 1f);
        //            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green, 1f);

        //        }
        //        else
        //        {
        //            if (stateMachine.CurrentState != patrolState)
        //                stateMachine.ChangeState(patrolState);


        //            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 1f);
        //        }
        //    }
        //}

        public void AttackPlayer()
        {
            stateMachine.ChangeState(attackState);
            //currentState = attackState;
            Debug.Log("Attacking player");
        }

    }
}
