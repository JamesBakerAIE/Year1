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
        public bool holdingInteract = false;
        private List<Collider> colliders = new List<Collider>();

        private IncreaseDownload increaseDownload = null;

        public RaycastHit hit;



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



            if (Time.timeScale != 0)
            {
                mouseX = Input.GetAxisRaw("Mouse X");
                mouseY = Input.GetAxisRaw("Mouse Y");



                interact = Input.GetButtonDown("Interact");

                holdingInteract = Input.GetButton("Interact");

                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
                {
                    player.playerAnimator.SetBool("Walking", true);
                    player.playerAnimator.SetFloat("Speed", 1);

                    player.leftArmAnimator.SetBool("Walking", true);
                    player.leftArmAnimator.SetFloat("Speed", 1);
                }
                else
                {
                    player.playerAnimator.SetBool("Walking", false);
                    //player.playerAnimator.SetFloat("Speed", 0);
                    
                    player.leftArmAnimator.SetBool("Walking", false);
                }

            }



            if (Time.timeScale == 0)
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
            else if (sprint)
            {
                stateMachine.ChangeState(player.sprintingState);
            }
            else if (interact)
            {
                Ray ray = new Ray(player.playerCamera.transform.position, player.playerCamera.transform.forward);




                if (Physics.Raycast(ray, out hit, player.interactRange, player.hideSpotLayerMask))
                {
                    if (hit.collider.isTrigger)
                    {
                        player.result = hit;
                        stateMachine.ChangeState(player.hidingState);
                        player.leftArmAnimator.SetBool("Grabbing", true);
                    }
                }



                if (Physics.Raycast(ray, out hit, player.pickupDistance, player.keycardLayerMask))
                {
                    if (hit.collider.isTrigger)
                    {
                        hit.collider.gameObject.SetActive(false);
                        player.keycardCount += 1;
                        player.leftArmAnimator.SetBool("Grabbing", true);
                    }
                }

                if (Physics.Raycast(ray, out hit, player.pickupDistance, player.keycardHolderLayerMask))
                {
                    if (player.keycardCount >= 1)
                    {
                        KeycardInput keycardInput = hit.collider.gameObject.GetComponent<KeycardInput>();
                        keycardInput.SpawnCard();
                        player.leftArmAnimator.SetBool("Grabbing", true);
                    }
                }

            }
            else if (holdingInteract)
            {
                Ray ray = new Ray(player.playerCamera.transform.position, player.playerCamera.transform.forward);


                Collider[] hitColliders = Physics.OverlapSphere(ray.origin, player.pickupRadius, player.computerLayerMask.value);

                if (hitColliders.Length > 0)
                {
                    
                    increaseDownload = hitColliders[0].GetComponentInChildren<IncreaseDownload>();

                    increaseDownload.Increase();
                }   
            }

            if (player.currentSprintTime <= player.maxSprintTime && player.currentSprintTime > 0)
            {
                player.currentSprintTime -= Time.deltaTime;
            }



            Ray ray2 = new Ray(player.playerCamera.transform.position, player.playerCamera.transform.forward);

            if (Physics.Raycast(ray2, out hit, player.interactRange, player.hideSpotLayerMask))
            {
                player.interactUI.SetActive(true);
            }
            else if(Physics.Raycast(ray2, out hit, player.pickupDistance, player.keycardLayerMask))
            {
                player.interactUI.SetActive(true);
            }
            else if(Physics.Raycast(ray2, out hit, player.pickupDistance, player.keycardHolderLayerMask))
            {
                player.interactUI.SetActive(true);
            }
            else
            {
                player.interactUI.SetActive(false);

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