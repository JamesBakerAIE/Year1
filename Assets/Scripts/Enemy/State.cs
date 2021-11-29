using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public class State : MonoBehaviour
    {
        public List<Transition> transitions;
        public float movementSpeed;
        public float hearingRadius;
        public  AudioClip enemySound;
        public bool isRunning = false;
        public bool isSearching = false;

        //[HideInInspector] public SphereCollider hearingCollider;

        //called when first switched to state
        public virtual void Enter()
        {

        }


        //normal update
        public virtual Vector3 DestinationUpdate(Vector3 enemyPosition)
        {
            return Vector3.zero;
        }

        public virtual Transform RotationUpdate()
        {
            return null;
        }

        //late update
        public virtual float GetSpeed()
        {
            return 0;
        }

        public virtual void Exit()
        {

        }

    }
}