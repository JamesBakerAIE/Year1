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
        State currentState;
        GameObject room;

        public NavMeshAgent enemyAgent;

        // Start is called before the first frame update
        void Start()
        {
            patrolState = gameObject.AddComponent<PatrolState>();
            chaseState = gameObject.AddComponent<ChaseState>();
            currentState = chaseState;
        }

        // Update is called once per frame
        void Update()
        {
            currentState = patrolState;
            enemyAgent.destination = currentState.UpdateAgent(this.transform.position);
        }

        public void SeenPlayer()
        {
            currentState = chaseState;
        }

    }
}
