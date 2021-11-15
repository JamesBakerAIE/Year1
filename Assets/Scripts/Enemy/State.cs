using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public class State : MonoBehaviour
    {
        public List<Transition> transitions;


        //called when first switched to state
        public virtual void Enter()
        {

        }


        //normal update
        public virtual Vector3 DestinationUpdate(Vector3 enemyPosition)
        {
            return Vector3.zero;
        }

        //late update
        public virtual float GetSpeed()
        {
            return 0;
        }

        public virtual void Exit()
        {

        }

    }
}