﻿using System.Collections;
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

        public NavMeshAgent enemyAgent;
        // Start is called before the first frame update
        void Start()
        {
            stateMachine = gameObject.GetComponent<StateMachine>();
            patrolState = gameObject.GetComponent<PatrolState>();
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform;

            //currentState = patrolState;
            stateMachine.Initialize(patrolState);
        }
        void Update()
        {
            Debug.Log(stateMachine.CurrentState.ToString());
            //Updates the enemy's destination based on the current state's logic
            enemyAgent.destination = stateMachine.CurrentState.LogicUpdate(this.transform.position);

            State newState = null;
            //check if any transition conditions are met
            foreach (Transition transition in stateMachine.CurrentState.transitions)
            {
                newState = transition.CheckTransition(this.transform.position, playerPosition.position);
                if(newState != null || newState == stateMachine.CurrentState)
                {
                    stateMachine.CurrentState = newState;
                    break;
                }
            }
        }

    }
}
