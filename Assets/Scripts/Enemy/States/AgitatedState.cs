using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
namespace EnemyAI
{
    public class AgitatedState : State
    {
        Vector3 playersLastPosition;
        public override void Enter()
        {
            Debug.Log("Entered patrol state");
            playersLastPosition = GameObject.FindObjectOfType<PlayerController>().transform.position;
        }
        public override Vector3 LogicUpdate(Vector3 enemyPosition)
        {
            return playersLastPosition;
        }
    }
}
