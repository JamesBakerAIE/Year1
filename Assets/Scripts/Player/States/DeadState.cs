using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class DeadState : State
    {
        public DeadState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void LateLogicUpdate()
        {
            base.LateLogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }

}
