using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public abstract class State : MonoBehaviour
    {
        protected PlayerController player;
        protected StateMachine stateMachine;

        protected float speed;

        public float horizontalInput;
        public float verticalInput;

        public float mouseX;
        public float mouseY;

        protected State(PlayerController player, StateMachine stateMachine)
        {
            this.player = player;
            this.stateMachine = stateMachine;
        }

        public virtual void Enter()
        {

        }

        public virtual void HandleInput()
        {

        }

        public virtual void LogicUpdate()
        {

        }

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
