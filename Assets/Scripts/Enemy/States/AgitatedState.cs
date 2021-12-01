using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
namespace EnemyAI
{
    public class AgitatedState : State
    {

        private void Start()
        {
            transitions.Add(timerTransition);
            transitions.Add(seenTransition);
            //hearingCollider = GetComponentInChildren<SphereCollider>();


        }

        public override void Enter()
        {
            if (enemyAudio.clip != enemySound)
            {
                enemyAudio.clip = enemySound;
                enemyAudio.Play();
                enemyAudio.loop = true;
            }
            isRunning = true;
            //hearingCollider.radius = hearingRadius;

        }
        public override void Exit()
        {

        }


        public override float GetSpeed()
        {
            return movementSpeed;
        }
        public override Vector3 DestinationUpdate(Vector3 enemyPosition)
        {
            return playerPosition.position;
        }
    }
}
