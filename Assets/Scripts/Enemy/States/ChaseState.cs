using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace EnemyAI
{
    public class ChaseState : State
    {

        private void Start()
        {
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
            //hearingCollider.radius = hearingRadius;
            isRunning = true;
        }
        public override void Exit()
        {

        }

        public override float GetSpeed()
        {
            return movementSpeed;
        }

        // Update is called once per frame
        public override Vector3 DestinationUpdate(Vector3 enemyPosition)
        {
            return playerPosition.position;
        }
    }
}