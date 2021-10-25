using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class CrouchingState : State
    {

        public CrouchingState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }
    }

}
