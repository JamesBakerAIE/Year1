using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace EnemyAI
{
    public class ChaseState : State
    {
        Transform player;

        SeenTransition seenTransition;
        private void Start()
        {
            seenTransition = GameObject.FindObjectOfType<SeenTransition>();
            transitions.Add(seenTransition);

            player = GameObject.FindObjectOfType<PlayerController>().gameObject.transform;

        }

        public override void Enter()
        {

        }
        public override void Exit()
        {

        }

        public override float GetSpeed()
        {
            return speed;
        }

        // Update is called once per frame
        public override Vector3 DestinationUpdate(Vector3 enemyPosition)
        {
            return player.position;
        }
    }
}