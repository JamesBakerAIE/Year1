using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Puzzle;

namespace Player
{
    public class WalkingState : State
    {
        private bool crouch;
        private bool sprint;
        public bool interact = false;
        private List<Collider> colliders = new List<Collider>();

        public WalkingState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            speed = player.walkSpeed;
            crouch = false;
        }
        public override void HandleInput()
        {
            base.HandleInput();
            verticalInput = Input.GetAxisRaw("Vertical");
            horizontalInput = Input.GetAxisRaw("Horizontal");

            crouch = Input.GetButtonDown("Crouch");
            sprint = Input.GetButtonDown("Sprint");

            if(Time.timeScale != 0)
            {
                mouseX = Input.GetAxisRaw("Mouse X");
                mouseY = Input.GetAxisRaw("Mouse Y");

                interact = Input.GetButtonDown("Interact");
            }

            if(Time.timeScale == 0)
            {
                mouseX = 0;
                mouseY = 0;
            }

        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (crouch)
            {
                stateMachine.ChangeState(player.crouchingState);
            }
            else if(sprint)
            {
                stateMachine.ChangeState(player.sprintingState);
            }
            else if(interact)
            {
                Ray ray = new Ray(player.playerCamera.transform.position, player.playerCamera.transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, player.interactRange, player.hideSpotLayerMask))
                {
                    if(hit.collider.isTrigger)
                    {
                        Debug.Log(hit.collider);
                        player.result = hit;
                        stateMachine.ChangeState(player.hidingState);
                    }


                }

                if(Physics.Raycast(ray, out hit, player.puzzleInteractRange, player.keycardHolderLayerMask))
                {
                    if(hit.collider.isTrigger)
                    {
                        Debug.Log("Test");
                        player.result = hit;
                        KeyInput keyInput = hit.collider.gameObject.GetComponent<KeyInput>();

                        keyInput.SetValues(keyInput.gameObject.transform.position);
                    }
                }

                Collider[] colliders = Physics.OverlapCapsule(player.playerCamera.transform.position, player.playerCamera.transform.position * player.pickupDistance, player.pickupRadius, player.keycardLayerMask);

                if (colliders != null && colliders.Length >= 1)
                {
                    colliders[0].gameObject.SetActive(false);
                    player.keycardCount += 1;
                    colliders = null;
                }

            }
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

    }


}
