using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace EnemyAI
{
    public class SearchState : State
    {
        // Start is called before the first frame update
        Transform playerPosition;
        TimerTransition timerTransition;
        public override void Enter()
        {
            //Debug.Log("Entered agitated state");
            //playersLastPosition = GameObject.FindObjectOfType<PlayerController>().transform.position;
            //timerTransition = GameObject.FindObjectOfType<TimerTransition>();
            //transitions.Add(timerTransition);
            Debug.Log("Entered agitated state");

        }

        private void Start()
        {
            playerPosition = GameObject.FindObjectOfType<PlayerController>().transform;
            timerTransition = GameObject.FindObjectOfType<TimerTransition>();
            transitions.Add(timerTransition);
        }
        public override Vector3 LogicUpdate(Vector3 enemyPosition)
        {
            return playerPosition.position;
        }
    }
}
