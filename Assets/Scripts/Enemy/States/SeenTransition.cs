using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public class SeenTransition : Transition
    {
        public Vector3 eyesOffset;
        public Vector3 directionOffset;
        Transition parentTransition;
        LayerMask ignoreLayer;

        private void Start()
        {
            parentTransition = GameObject.FindObjectOfType<Transition>();
            ignoreLayer = Physics.IgnoreRaycastLayer;
        }
        public override void Enter()
        {
            //UPDATING ENTER AND EXIT FOR STATES AND TRANSITIONS
        }
        public override void Exit()
        {
            //UPDATING ENTER AND EXIT FOR STATES AND TRANSITIONS
        }

        public override State CheckTransition(Vector3 enemyPositon, Vector3 playerPosition)
        {

            bool wasInFOV = parentTransition.inDirectFOV;

            Vector3 direction = (enemyPositon - playerPosition);
            RaycastHit hit;

            State selectedState = null;

            //an array that shoots in the player's direction
            Ray ray = new Ray(enemyPositon + eyesOffset, -direction + directionOffset);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreLayer))
            {
                //Player is in direct sight to the enemy
                if (hit.collider.tag == "Player" && parentTransition.inFOV)
                {

                    //In attack range
                    Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green, 1f);
                    if (parentTransition.inAttack)
                    {
                        Debug.Log("attack");
                        parentTransition.inDirectAttack = true;
                        parentTransition.inDirectFOV = true;
                        selectedState = parentTransition.attackState;
                    }
                    //In view range
                    else if (parentTransition.inFOV)
                    {
                        parentTransition.inDirectFOV = true;
                        selectedState = parentTransition.chaseState;
                    }

                }
                //Player isn;t in direct sight of the enemy
                else 
                {
                    Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 1f);

                    parentTransition.inDirectFOV = false;
                }

                //Last time this was called it was in field of view, now it isn't so become agitated
                if(parentTransition.inDirectFOV == false && wasInFOV == true)
                {
                    parentTransition.inDirectFOV = false;
                    parentTransition.inDirectAttack = false;
                    selectedState = parentTransition.agitatedState;

                }

                //update previous FOV
                wasInFOV = parentTransition.inDirectFOV;

            }

            return selectedState;
        }

    }
}

