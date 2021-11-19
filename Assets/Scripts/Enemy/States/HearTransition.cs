using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace EnemyAI
{
    public class HearTransition : Transition
    {
        Transition parentTransition;
        State currentState;

        SphereCollider hearingRadius;
        private void Start()
        {
            parentTransition = GameObject.FindObjectOfType<Transition>();
        }



        public override void Enter()
        {
            currentState = null;
        }
        public override void Exit()
        {



        }



        public override State CheckTransition(Vector3 enemyPositon, Vector3 playerPosition)
        {
            if (FindObjectOfType<SearchState>().foundPlayer == true)
                return currentState = parentTransition.attackState;
            return currentState;
        }



    }
}