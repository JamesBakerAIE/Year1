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

        private void Start()
        {
            parentTransition = GameObject.FindObjectOfType<Transition>();
        }

        public override State CheckTransition(Vector3 enemyPositon, Vector3 playerPosition)
        {
            if (parentTransition.inFOV == false && inFOV == false)
                return null;

            inFOV = parentTransition.inFOV;


            //ignores colliders on this specific layer
            LayerMask ignoreLayer = Physics.IgnoreRaycastLayer;
            Vector3 direction = (enemyPositon - playerPosition);
            RaycastHit hit;

            Ray ray = new Ray(enemyPositon + eyesOffset, -direction + directionOffset);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreLayer))
            {
                //Player is in direct sight to the enemy
                if (hit.collider.tag == "Player")
                {
                    Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green, 1f);
                    if (inAttack)
                    {
                        Debug.Log("attack");
                        parentTransition.inDirectAttack = true;
                        return parentTransition.attackState;
                    }
                    else
                    {
                        parentTransition.inDirectFOV = true;
                        return parentTransition.chaseState;
                    }
                }
                //Player isn't in direct sight to the enemy
                else
                {
                    Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 1f);
                    parentTransition.inDirectFOV = false;
                    parentTransition.inDirectAttack = false;

                }

                if(parentTransition.inDirectFOV == false && inFOV)
                    return parentTransition.agitatedState;
            }
            return null;
        }
    }
}

