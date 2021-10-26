using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EnemyAI
{
    public class AttackState : State
    {
        // Start is called before the first frame update
        public override void Enter()
        {
            Debug.Log("Entered attack state");
        }

        // Update is called once per frame
        //public override Vector3 UpdateAgent(Vector3 enemyPosition)
        //{
        //    return Vector3.zero;
        //}
    }
}
