using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public class AgitatedState : State
    {
        // Start is called before the first frame update
        public override void Enter()
        {
            Debug.Log("Entered agitated state");
        }

        // Update is called once per frame
        public override Vector3 LogicUpdate(Vector3 enemyPosition)
        {
            return Vector3.zero;
        }
    }
}
