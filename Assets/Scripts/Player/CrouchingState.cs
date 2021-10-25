using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{

    public class CrouchingState : State
    {
        private bool crouchHeld;
        private bool belowCeiling;

        private float timeElapsed = 0f;
        private float startValue = 0;
        private float endValue = -0.378f;
        private float valueToLerp = 0f;
        private bool isChangingPosition = false;
        public CrouchingState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            speed = player.crouchSpeed;
            player.playerCollider.size = new Vector3(player.playerColliderNormalSize.x, player.playerCrouchColliderHeight, player.playerColliderNormalSize.z);
            crouchHeld = false;

            
        }
        public override void HandleInput()
        {
            base.HandleInput();
            verticalInput = Input.GetAxisRaw("Vertical");
            horizontalInput = Input.GetAxisRaw("Horizontal");

            crouchHeld = Input.GetButtonUp("Crouch");

            mouseX = Input.GetAxisRaw("Mouse X");
            mouseY = Input.GetAxisRaw("Mouse Y");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (!(crouchHeld || !belowCeiling))
            {
                stateMachine.ChangeState(player.walkingState);
            }
            StartCoroutine(Crouch());


        }

        public override void LateLogicUpdate()
        {
            base.LateLogicUpdate();
            Vector2 mouseDelta = new Vector2(mouseX, mouseY);
            player.cameraPitch -= mouseDelta.y * player.mouseSensitivity;
            player.cameraPitch = Mathf.Clamp(player.cameraPitch, -90f, 90f);
            player.playerCamera.localEulerAngles = Vector3.right * player.cameraPitch;

            player.transform.Rotate(Vector3.up * mouseDelta.x * player.mouseSensitivity);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            if (player.controller.isGrounded && player.velocity.y < 0)
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
            belowCeiling = player.CheckCollisionOverlap(player.transform.position + Vector3.up * player.playerColliderNormalSize.y);
            base.Exit();
        }

        public IEnumerator Crouch()
        {
            if (isChangingPosition)
            {
                yield break;
            }

            isChangingPosition = true;

            float startingHeight = player.playerCamera.transform.position.y;

            float endHeight = 0f;
            if(crouchHeld)
            {
                endHeight = startingHeight + player.cameraCrouchDistance;
            }
            else
            {
                endHeight = startingHeight - player.cameraCrouchDistance;
            }

            while(player.playerCamera.transform.position.y != endHeight)
            {
                if(crouchHeld)
                {
                    transform.Translate(Vector3.up * player.crouchSpeed * Time.deltaTime);

                    //True if we reached our goal
                    if (transform.position.y >= endHeight)
                    {
                        //Make sure we are EXACTLY at the EndHeight
                        transform.position = new Vector3(transform.position.x, endHeight, transform.position.z);
                        Debug.Log("COMPLETE!");
                    }
                }
                else
                {
                    transform.Translate(Vector3.down * player.crouchSpeed * Time.deltaTime);

                    if(player.playerCamera.transform.position.y <= endHeight)
                    {
                        transform.position = new Vector3(player.playerCamera.transform.position.x, endHeight, player.playerCamera.transform.position.z);

                        Debug.Log("COMPLETE!");
                    }
                }

                yield return null;
            }


        }
    }

}