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

        public virtual void HandleInput()
        {

        }

        //normal update
        public virtual Vector3 LogicUpdate(Vector3 enemyPosition)
        {
            return Vector3.zero;
        }

        //late update
        public virtual void LateLogicUpdate()
        {

        }

        public virtual void PhysicsUpdate()
        {

        }

        public virtual void Exit()
        {

        }

    }
}