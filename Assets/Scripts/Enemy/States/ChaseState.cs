using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace EnemyAI
{
    public class ChaseState : State
    {
        public override void Enter()
        {
            Debug.Log("Entered chase state");
        }

        // Update is called once per frame
        public override Vector3 LogicUpdate(Vector3 enemyPosition)
        {
            return GameObject.FindObjectOfType<PlayerController>().gameObject.transform.position;
        }
    }
}