using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{

    public class StateMachine : MonoBehaviour
    {
        public State CurrentState;

        public void Initialize(State currentState)
        {
            CurrentState = currentState;
            currentState.Enter();
        }

        public void ChangeState(State newState)
        {
            CurrentState.Exit();
            foreach (Transition transition in CurrentState.transitions)
                transition.Exit();

            CurrentState = newState;
            newState.Enter();
            foreach (Transition transition in CurrentState.transitions)
                transition.Enter();
        }


    }
}
