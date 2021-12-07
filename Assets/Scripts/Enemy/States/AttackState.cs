using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers.UIManager;
using Player;

namespace EnemyAI
{
    public class AttackState : State
    {
        private void Start()
        {
            GetComponents();
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

        // Start is called before the first frame update
        public override void Enter()
        {
            FindObjectOfType<UIManager>().GameIsOver = true;
            if (enemyAudio.clip != enemySound)
            {
                enemyAudio.Stop();
            }

            //delete after sounds and everything have happened
            if (searchState.foundPlayer)
            {
                Destroy(this.gameObject);
            }

            Debug.Log("Player is dead");
            isAttacking = true;
            //hearingCollider.radius = hearingRadius;

        }

        public override Vector3 RotationUpdate()
        {
            return playerPosition.position;
 
        }
        public override void Exit()
        {

        }

        public override float GetSpeed()
        {
            return movementSpeed;
        }
    }
}
