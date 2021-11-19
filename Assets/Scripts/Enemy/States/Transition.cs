using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public class Transition : MonoBehaviour
    {
        public bool inFOV = false;
        public bool inDirectFOV = false;
        public bool inAttack = false;
        public bool inDirectAttack = false;
        public bool inHearingRange = false;

        State changeState;
        [HideInInspector] public ChaseState chaseState;
        [HideInInspector] public PatrolState patrolState;
        [HideInInspector] public AttackState attackState;
        [HideInInspector] public AgitatedState agitatedState;
        [HideInInspector] public SearchState searchState;

        private void Awake()
        {
            chaseState = GameObject.FindObjectOfType<ChaseState>();
            patrolState = GameObject.FindObjectOfType<PatrolState>();
            attackState = GameObject.FindObjectOfType<AttackState>();
            agitatedState = GameObject.FindObjectOfType<AgitatedState>();
            searchState = GameObject.FindObjectOfType<SearchState>();
        }

        public virtual void Enter()
        {

        }

        public virtual void Exit()
        {

        }


        public virtual State CheckTransition(Vector3 enemyPositon, Vector3 playerPosition)
        {
            return null;
        }

    }
}
