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

        State changeState;
        public ChaseState chaseState;
        public PatrolState patrolState;
        public AttackState attackState;
        public AgitatedState agitatedState;
        public SearchState searchState;

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
