﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{

    public class CrouchingState : State
    {
        private bool crouchHeld;
        private bool belowCeiling;

        private float startValue = 0.599f;
        private float endValue = 0f;    
        public CrouchingState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            speed = player.crouchSpeed;

            // Set collider info for crouching
            Vector3 newHeight = new Vector3(player.controller.bounds.size.x, player.controller.bounds.size.y / 2, player.controller.bounds.size.z);
            Vector3 newCenter = new Vector3(player.controller.center.x, player.controller.center.y * 2, player.controller.center.z);

            player.controller.height = newHeight.y;
            player.controller.center = newCenter;
            crouchHeld = false;

            startValue = player.playerCamera.localPosition.y;

            endValue = player.playerCamera.transform.localPosition.y - player.cameraCrouchDistance;
        }
        public override void HandleInput()
        {
            base.HandleInput();
            verticalInput = Input.GetAxisRaw("Vertical");
            horizontalInput = Input.GetAxisRaw("Horizontal");

            crouchHeld = Input.GetButton("Crouch");
            mouseX = Input.GetAxisRaw("Mouse X");
            mouseY = Input.GetAxisRaw("Mouse Y");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            Crouch();
        }

        public override void LateLogicUpdate()
        {
            base.LateLogicUpdate();
            
            // Mouse looking

            Vector2 mouseDelta = new Vector2(mouseX, mouseY);
            player.cameraPitch -= mouseDelta.y * player.mouseSensitivity;
            player.cameraPitch = Mathf.Clamp(player.cameraPitch, -90f, 90f);
            player.playerCamera.localEulerAngles = Vector3.right * player.cameraPitch;

            player.transform.Rotate(Vector3.up * mouseDelta.x * player.mouseSensitivity);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            // Check if below ceiling
            belowCeiling = player.CheckCollisionOverlap(player.transform.position + Vector3.up * player.playerColliderNormalSize.y);

            // Check if player is grounded then set velocity to -2f (this gives the velocity time to reset)
            if (player.controller.isGrounded && player.velocity.y < 0)
            {
                player.velocity.y = -2f;
            }

            // Player movement
            Vector2 inputDir = new Vector2(horizontalInput, verticalInput);
            inputDir.Normalize();

            Vector3 move = (player.transform.forward * inputDir.y + player.transform.right * inputDir.x) * speed;
            player.controller.Move(move * Time.deltaTime);

            // Apply gravity
            player.velocity.y += Physics.gravity.y * player.gravityMultiplier * Time.deltaTime;

            player.controller.Move(player.velocity * Time.deltaTime);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public void Crouch()
        {
            Vector3 camPos = player.playerCamera.localPosition;
            if (crouchHeld)
            {        
                // Move the camera towards the crouch location
                camPos.y = Mathf.MoveTowards(camPos.y, endValue, player.crouchSpeed * Time.deltaTime);
                player.playerCamera.localPosition = camPos;

            }
            else
            {
                if(!belowCeiling)
                {
                    // Move the camera back to the head of the player
                    camPos.y = Mathf.MoveTowards(camPos.y, startValue, player.crouchSpeed * Time.deltaTime);
                    player.playerCamera.localPosition = camPos;
                }


            }

            if(player.playerCamera.localPosition.y == startValue)
            {
                if (!(crouchHeld || belowCeiling))
                {
                    // Set collider info for crouching
                    Vector3 newHeight = new Vector3(player.controller.bounds.size.x, player.controller.bounds.size.y * 2, player.controller.bounds.size.z);
                    Vector3 newCenter = new Vector3(player.controller.center.x, player.controller.center.y * 2, player.controller.center.z);
                   
                    player.controller.center = newCenter;
                    player.controller.height = newHeight.y;

                    stateMachine.ChangeState(player.walkingState);
                }   
            }




        }
    }


}