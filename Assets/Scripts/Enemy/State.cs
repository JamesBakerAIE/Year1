using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace EnemyAI
{
    public class State : MonoBehaviour
    {
        [Tooltip("Transitions This State Has")]
        public List<Transition> transitions;

        public float movementSpeed;
        //public float hearingRadius;
        [Tooltip("Sound To Play When Entering State")]
        public AudioClip enemySound;

        [Header("Current Status'")]
        public bool isRunning = false;
        public bool isSearching = false;
        public bool isAttacking = false;

        protected AudioSource enemyAudio;
        protected Transform playerPosition;

        //All the different states and transitions
        protected StateMachine enemyStateMachine;
        protected PatrolState patrolState;
        protected SearchState searchState;
        protected AgitatedState agitatedState;
        protected ChaseState chaseState;
        protected AttackState attackState;

        protected SeenTransition seenTransition;
        protected LockerTransition lockerTransition;
        protected TimerTransition timerTransition;


        protected GameObject attackRange;

        //[HideInInspector] public SphereCollider hearingCollider;
        public void Start()
        {
            //Initializing variables here for every state
            GetComponents();
        }

        void GetComponents()
        {
            //Initializing variables here for every state
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
        //called when first switched to state
        public virtual void Enter()
        {

        }


        //Used to update the enemy AI's navmesh agent each frame
        public virtual Vector3 DestinationUpdate(Vector3 enemyPosition)
        {
            return Vector3.zero;
        }

        //Used to update the enemny vAI's rotation in specific instances
        public virtual Vector3 RotationUpdate()
        {
            return Vector3.zero;
        }


        public virtual float GetSpeed()
        {
            return 0;
        }

        public virtual void Exit()
        {

        }

    }
}