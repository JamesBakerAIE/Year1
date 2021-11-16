using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public class TimerTransition : Transition
    {
        Transition parentTransition;
        State currentState;
        private void Start()
        {
            parentTransition = GameObject.FindObjectOfType<Transition>();
        }

        public float timeAgitated = 5;
        public float timeElapsed = 0;

        public override void Enter()
        {
            timeElapsed = 0;
        }
        public override void Exit()
        {
            //UPDATING ENTER AND EXIT FOR STATES AND TRANSITIONS
        }

        public override State CheckTransition(Vector3 enemyPositon, Vector3 playerPosition)
        {
            currentState = null;

            if(timeElapsed >= timeAgitated)
            {
                timeElapsed = 0;
                currentState = parentTransition.searchState;
            }
            else
            {
                timeElapsed += Time.deltaTime;
            }
            return currentState;
        }

    }
}
