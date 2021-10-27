using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class HidingState : State
    {

        private bool interact = false;
        private HideSpot[] hideSpots;


        public HidingState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
        {
            hideSpots = FindObjectsOfType<HideSpot>();
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void HandleInput()
        {
            base.HandleInput();

            interact = Input.GetButtonDown("Interact");
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

            player.gameObject.transform.position = Vector3.MoveTowards(player.gameObject.transform.position, 
                player.result.collider.gameObject.GetComponentInParent<HideSpot>().gameObject.transform.position, player.walkToLockerTime * Time.deltaTime);
        }

        public override void Exit()
        {
            base.Exit();
        }

    }

}
