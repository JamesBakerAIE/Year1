using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace EnemyAI
{
    public class ChaseState : State
    {

        private void Start()
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

            transitions.Add(seenTransition);
            //hearingCollider = GetComponentInChildren<SphereCollider>();

        }

        public override void Enter()
        {
            if (enemyAudio.clip != enemySound)
            {
                enemyAudio.clip = enemySound;
                enemyAudio.Play();
                enemyAudio.loop = true;
            }
            //hearingCollider.radius = hearingRadius;
            isRunning = true;
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
            return playerPosition.position;
        }
    }
}