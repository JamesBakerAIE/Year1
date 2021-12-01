using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyAI
{

    public class Enemy : MonoBehaviour
    {
        public StateMachine stateMachine;
        PatrolState patrolState;
        Transform playerPosition;
        public Animator animator;

        public NavMeshAgent enemyAgent;
        // Start is called before the first frame update
        void Start()
        {
            stateMachine = gameObject.GetComponent<StateMachine>();
            patrolState = gameObject.GetComponent<PatrolState>();
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
            //currentState = patrolState;
            stateMachine.Initialize(patrolState);
            stateMachine.ChangeState(patrolState);
        }
        void Update()
        {
            Debug.Log(stateMachine.CurrentState.ToString());
            //Updates the enemy's destination based on the current state's logic
            enemyAgent.destination = stateMachine.CurrentState.DestinationUpdate(this.transform.position);
            if(stateMachine.CurrentState.RotationUpdate() != Vector3.zero)
            {
                this.transform.LookAt(stateMachine.CurrentState.RotationUpdate());
                //this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, stateMachine.CurrentState.RotationUpdate().rotation, 10* 15 * Time.deltaTime);
            }

            enemyAgent.speed = stateMachine.CurrentState.GetSpeed();
            animator.SetBool("Running", stateMachine.CurrentState.isRunning);
            animator.SetBool("Searching", stateMachine.CurrentState.isSearching);
            animator.SetBool("Attacking", stateMachine.CurrentState.isAttacking);
            State newState = null;
            //check if any transition conditions are met
            foreach (Transition transition in stateMachine.CurrentState.transitions)
            {
                Debug.Log("First transition: " + stateMachine.CurrentState.transitions);
                Debug.Log("Current Transition: " + transition);
                newState = transition.CheckTransition(this.transform.position, playerPosition.position);
                if(newState != null || newState == stateMachine.CurrentState)
                {
                    stateMachine.ChangeState(newState);
                    break;
                }
            }
        }

    }
}
