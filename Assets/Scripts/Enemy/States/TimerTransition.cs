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

        public override State CheckTransition(Vector3 enemyPositon, Vector3 playerPosition)
        {
            currentState = null;

            if (parentTransition.inDirectAttack)
                currentState = parentTransition.attackState;
            else if(parentTransition.inDirectFOV)
                currentState = parentTransition.patrolState;

            if(timeElapsed >= timeAgitated)
            {
                timeElapsed = 0;
                currentState = parentTransition.patrolState;
            }
            else
            {
                timeElapsed += Time.deltaTime;
            }
            return currentState;
        }

    }
}
