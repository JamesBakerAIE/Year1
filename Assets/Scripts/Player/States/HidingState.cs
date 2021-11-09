using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class HidingState : State
    {

        private bool interact = false;
        private bool crouch;
        private bool sprint;

        private bool enteringLocker = false;
        private bool leavingLocker = false;
        private bool lookToFrontOfLocker = false;

        public GameObject lockerInsideOf = null;
        public HidingState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
        {

        }

        public override void Enter()
        {
            lockerInsideOf.GetComponentInParent<HideSpot>().hasPlayer = true;
            base.Enter();
        }

        public override void HandleInput()
        {
            base.HandleInput();

            interact = Input.GetButtonDown("Interact");
            crouch = Input.GetButtonDown("Crouch");
            sprint = Input.GetButtonDown("Sprint");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(interact && enteringLocker == false && lookToFrontOfLocker)
            {
                leavingLocker = true;
                lockerInsideOf.GetComponentInParent<HideSpot>().hasPlayer = false;
                //stateMachine.ChangeState(player.walkingState);
            }

            if(leavingLocker)
            {
                //Vector3 move = player.transform.forward * player.lockerExitDistance;
                //player.transform.position += move * Time.deltaTime;
                player.transform.position = Vector3.MoveTowards(player.transform.position, player.transform.position + player.transform.forward * player.lockerExitDistance, player.walkToLockerTime * Time.deltaTime);

                leavingLocker = false;

                if(crouch)
                {
                    stateMachine.ChangeState(player.crouchingState);
                }
                else if(sprint)
                {
                    stateMachine.ChangeState(player.sprintingState);
                }
                else
                {
                    stateMachine.ChangeState(player.walkingState);
                }

            }


            
        }

        public override void LateLogicUpdate()
        {
            base.LateLogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            Vector3 target = new Vector3(player.result.collider.gameObject.GetComponentInParent<HideSpot>().gameObject.transform.position.x, 
                player.result.collider.GetComponentInParent<HideSpot>().gameObject.transform.position.y,
                player.result.collider.gameObject.GetComponentInParent<HideSpot>().gameObject.transform.position.z);

            Transform lookTransform = player.result.collider.gameObject.transform;

            lookTransform.position = new Vector3(player.result.collider.gameObject.transform.position.x, player.gameObject.transform.position.y,
                player.result.collider.gameObject.transform.position.z);

            if (leavingLocker == false)
            {
                enteringLocker = true;

                player.gameObject.transform.position = Vector3.MoveTowards(player.gameObject.transform.position, target, player.walkToLockerTime * Time.fixedDeltaTime);


                player.transform.LookAt(lookTransform);
            }

            if(player.gameObject.transform.position == target)
            {
                enteringLocker = false;
            }

            if (player.transform.rotation.x == lookTransform.rotation.x && player.transform.rotation.z == lookTransform.rotation.z)
            {
                lookToFrontOfLocker = true;
            }
            else
            {
                lookToFrontOfLocker = false;
            }


        }

        public override void Exit()
        {
            base.Exit();
        }

    }

}
