using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class SprintingState : State
    {
        private bool walk;
        private bool interact;
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

            if(Time.timeScale != 0)
            {

                mouseX = Input.GetAxisRaw("Mouse X");
                mouseY = Input.GetAxisRaw("Mouse Y");

                interact = Input.GetButtonDown("Interact");

                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
                {
                    //player.playerAnimator.SetBool("Walking", true);
                   // player.playerAnimator.SetFloat("Speed", 4);
                }
                else
                {
                   // player.playerAnimator.SetBool("Walking", false);
                    //player.playerAnimator.SetFloat("Speed", 0);

                }
            }
            walk = Input.GetButtonUp("Sprint");

        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (walk)
            {
                stateMachine.ChangeState(player.walkingState);
            }
            else if (interact)
            {
                Ray ray = new Ray(player.transform.position, player.transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, player.interactRange, player.hideSpotLayerMask))
                {
                    if (hit.collider.isTrigger)
                    {

                        player.result = hit;
                        stateMachine.ChangeState(player.hidingState);
                    }

                }
            }

            player.currentSprintTime += Time.deltaTime;
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

            if(player.currentSprintTime < player.maxSprintTime)
            {
                // Check if player is grounded then set velocity to -2f (this gives the velocity time to reset)
                if (player.controller.isGrounded && player.velocity.y < 0)
                {
                    player.velocity.y = -2f;
                }

                // Normalized movement
                Vector2 inputDir = new Vector2(horizontalInput, verticalInput);
                inputDir.Normalize();

                Vector3 move = (player.transform.forward * inputDir.y + player.transform.right * inputDir.x) * speed;
                player.controller.Move(move * Time.deltaTime);

                // Apply gravity
                player.velocity.y += Physics.gravity.y * player.gravityMultiplier * Time.deltaTime;

                player.controller.Move(player.velocity * Time.deltaTime);
            }
            else
            {
                stateMachine.ChangeState(player.walkingState);
            }
           
        }

        public override void Exit()
        {
            base.Exit();
        }
    }

}
