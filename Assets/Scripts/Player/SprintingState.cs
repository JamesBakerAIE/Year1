﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class SprintingState : State
    {
        private bool walk;
        public SprintingState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            speed = player.sprintSpeed;
            walk = false;
        }
        public override void HandleInput()
        {
            base.HandleInput();
            verticalInput = Input.GetAxisRaw("Vertical");
            horizontalInput = Input.GetAxisRaw("Horizontal");

            walk = Input.GetButtonUp("Sprint");

            mouseX = Input.GetAxisRaw("Mouse X");
            mouseY = Input.GetAxisRaw("Mouse Y");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if(walk)
            {
                stateMachine.ChangeState(player.walkingState);
            }
        }

        public override void LateLogicUpdate()
        {
            base.LateLogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            if(player.controller.isGrounded && player.velocity.y < 0)
            {
                player.velocity.y = -2f;
            }

            Vector2 inputDir = new Vector2(horizontalInput, verticalInput);
            inputDir.Normalize();

            Vector3 move = (player.transform.forward * inputDir.y + player.transform.right * inputDir.x) * speed;
            player.controller.Move(move * Time.deltaTime);


            player.velocity.y += Physics.gravity.y * player.gravityMultiplier * Time.deltaTime;

            player.controller.Move(player.velocity * Time.deltaTime);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }

}
