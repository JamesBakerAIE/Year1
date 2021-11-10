using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
namespace EnemyAI
{
    public class AgitatedState : State
    {
        Transform playerPosition;
        TimerTransition timerTransition;
        SeenTransition seenTransition;
        public override void Enter()
        {
            //Debug.Log("Entered agitated state");
            //playersLastPosition = GameObject.FindObjectOfType<PlayerController>().transform.position;
            //timerTransition = GameObject.FindObjectOfType<TimerTransition>();
            //transitions.Add(timerTransition);


        }

        private void Start()
        {
            playerPosition = GameObject.FindObjectOfType<PlayerController>().transform;
            timerTransition = GameObject.FindObjectOfType<TimerTransition>();
            seenTransition = GameObject.FindObjectOfType<SeenTransition>();
            transitions.Add(timerTransition);
            transitions.Add(seenTransition);
        }
        public override Vector3 LogicUpdate(Vector3 enemyPosition)
        {
            return playerPosition.position;
        }
    }
}
