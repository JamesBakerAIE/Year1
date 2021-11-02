using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public class TimerTransition : Transition
    {

        State changeState;

        Transition parentTransition;
        private void Start()
        {
            parentTransition = GameObject.FindObjectOfType<Transition>();
        }

        public override State CheckTransition(Vector3 enemyPositon, Vector3 playerPosition)
        {
            if (parentTransition.inFOV == false && parentTransition.inAttack == false)
            {
                StartCoroutine(PatrolledDelay());
                return parentTransition.patrolState;
            }
            else if (inAttack)
                return parentTransition.attackState;
            else
                return parentTransition.patrolState;
        }

        IEnumerator PatrolledDelay()
        {
            Debug.Log("Patrolling");
            yield return new WaitForSeconds(5f);
            Debug.Log("Stop Patrolling");
        }
    }
}
