using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace EnemyAI
{
    public class State : MonoBehaviour
    {
        public List<Transition> transitions;

        public float movementSpeed;
        public float hearingRadius;
        public  AudioClip enemySound;
        public bool isRunning = false;
        public bool isSearching = false;
        public bool isAttacking = false;
        public AudioSource enemyAudio;
        public Transform playerPosition;

        public StateMachine enemyStateMachine;

        public PatrolState patrolState;
        public SearchState searchState;
        public AgitatedState agitatedState;
        public ChaseState chaseState;
        public AttackState attackState;

        public SeenTransition seenTransition;
        public LockerTransition lockerTransition;
        public TimerTransition timerTransition;


        public GameObject attackRange;

        //[HideInInspector] public SphereCollider hearingCollider;
        public void Start()
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


        //normal update
        public virtual Vector3 DestinationUpdate(Vector3 enemyPosition)
        {
            return Vector3.zero;
        }

        public virtual Vector3 RotationUpdate()
        {
            return Vector3.zero;
        }

        //late update
        public virtual float GetSpeed()
        {
            return 0;
        }

        public virtual void Exit()
        {

        }

    }
}