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
            if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<AudioSource>().clip != enemySound)
            {
                GameObject.FindGameObjectWithTag("Enemy").GetComponent<AudioSource>().clip = enemySound;
                GameObject.FindGameObjectWithTag("Enemy").GetComponent<AudioSource>().Play();
                GameObject.FindGameObjectWithTag("Enemy").GetComponent<AudioSource>().loop = true;
            }
        }
        public override void Exit()
        {

        }


        public override float GetSpeed()
        {
            return speed;
        }
        public override Vector3 DestinationUpdate(Vector3 enemyPosition)
        {
            return playerPosition.position;
        }
    }
}
