using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace EnemyAI
{
    public class ChaseState : State
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        public override Vector3 UpdateAgent(Vector3 enemyPosition)
        {
            return GameObject.FindObjectOfType<PlayerController>().gameObject.transform.position;
        }
    }
}