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
        public override State CheckTransition(Vector3 enemyPositon, Vector3 playerPosition)
        {
            if (parentTransition.inDirectAttack)
                currentState = parentTransition.attackState;
            else if(parentTransition.inDirectFOV)
                currentState = parentTransition.patrolState;

            StartCoroutine(PatrolledDelay());

            return currentState;
        }

        IEnumerator PatrolledDelay()
        {
            Debug.Log("Patrolling");
            yield return new WaitForSeconds(timeAgitated);
            currentState = parentTransition.patrolState;
            Debug.Log("Stop Patrolling");
        }
    }
}
