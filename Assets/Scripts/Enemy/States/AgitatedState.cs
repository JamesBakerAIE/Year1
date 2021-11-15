using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
namespace EnemyAI
{
    public class AgitatedState : State
    {
        public float stateSpeed;
        Transform playerPosition;
        TimerTransition timerTransition;
        SeenTransition seenTransition;

        private void Start()
        {
            playerPosition = GameObject.FindObjectOfType<PlayerController>().transform;
            timerTransition = GameObject.FindObjectOfType<TimerTransition>();
            seenTransition = GameObject.FindObjectOfType<SeenTransition>();
            transitions.Add(timerTransition);
            transitions.Add(seenTransition);
        }

        public override void Enter()
        {

        }
        public override void Exit()
        {

        }


        public override float GetSpeed()
        {
            return stateSpeed;
        }
        public override Vector3 DestinationUpdate(Vector3 enemyPosition)
        {
            return playerPosition.position;
        }
    }
}
