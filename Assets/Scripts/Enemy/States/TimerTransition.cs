using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public class TimerTransition : Transition
    {

        State changeState;

        Transition parentTransition;
        State currentState;
        private void Start()
        {
            parentTransition = GameObject.FindObjectOfType<Transition>();
        }

        public float timer = 500;
        public float currentTime;
        public override State CheckTransition(Vector3 enemyPositon, Vector3 playerPosition)
        {
            currentState = parentTransition.agitatedState;
            if (parentTransition.inDirectFOV == false && parentTransition.inDirectAttack == false)
            {
                currentTime--;
            }
            else if (parentTransition.inDirectAttack)
                currentState = parentTransition.attackState;
            else if(parentTransition.inDirectFOV)
                currentState = parentTransition.patrolState;

            if (currentTime < 0)
            {
                currentState = parentTransition.patrolState;
                currentTime = timer;
            }

            return currentState;
        }

        //IEnumerator PatrolledDelay()
        //{
        //    Debug.Log("Patrolling");
        //    yield return new WaitForSeconds(5f);
        //    currentState = parentTransition.patrolState;
        //    Debug.Log("Stop Patrolling");
        //}
    }
}
