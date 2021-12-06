using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
namespace EnemyAI
{
    public class AgitatedState : State
    {

        private void Start()
        {
            GetComponents();

            transitions.Add(timerTransition);
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
            isRunning = true;
            //hearingCollider.radius = hearingRadius;

        }
        public override void Exit()
        {

        }


        public override float GetSpeed()
        {
            return movementSpeed;
        }
        public override Vector3 DestinationUpdate(Vector3 enemyPosition)
        {
            return playerPosition.position;
        }
    }
}
