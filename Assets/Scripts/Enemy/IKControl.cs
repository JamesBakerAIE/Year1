using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI {

    [RequireComponent(typeof(Animator))]

    public class IKControl : MonoBehaviour
    {
        protected Animator animator;

        public Transform clawObject = null;
        public Transform grabObject = null;

        public bool isGrabbingObject;
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
        }


        void OnAnimatorIK()
        {
            if(isGrabbingObject == true)
            {
                //sets the grabobject's position
                if(grabObject != null)
                {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(grabObject.position);
                }
                //sets the claws target position and destination
                if(clawObject != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, clawObject.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, clawObject.rotation);

                }
            }
        }
    }
}
