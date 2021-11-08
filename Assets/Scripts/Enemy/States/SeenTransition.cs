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
            //if (parentTransition.inFOV == false)
            //    return null;

            if (parentTransition.inFOV == false)
            {
                if (inDirectFOV == true && parentTransition.inDirectFOV == false && parentTransition.inDirectAttack == false)
                {
                    Debug.Log("SHould be agitated");
                    inDirectFOV = parentTransition.inDirectFOV;
                    return parentTransition.agitatedState;
                }
            }


            //ignores colliders on this specific layer
            LayerMask ignoreLayer = Physics.IgnoreRaycastLayer;
            Vector3 direction = (enemyPositon - playerPosition);
            RaycastHit hit;

            State selectedState = null;

            if (parentTransition.inFOV)
            {
                Ray ray = new Ray(enemyPositon + eyesOffset, -direction + directionOffset);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreLayer))
                {
                    //Player is in direct sight to the enemy
                    if (hit.collider.tag == "Player")
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
                            inDirectFOV = true;
                            selectedState = parentTransition.chaseState;
                        }

                    }

                    //was in directFOV, not anymore
                    //if (inDirectFOV == true && parentTransition.inDirectFOV == false && parentTransition.inDirectAttack == false)
                    //{
                    //    Debug.Log("SHould be agitated");
                    //    selectedState = parentTransition.agitatedState;
                    //}

                    //if (inFOV == true && parentTransition.inFOV == false && parentTransition.inDirectAttack == false)
                    //{
                    //    Debug.Log("SHould be agitated");
                    //    selectedState = parentTransition.agitatedState;
                    //}
                    if (inDirectFOV == true && parentTransition.inDirectFOV == false && parentTransition.inDirectAttack == false)
                    {
                        Debug.Log("SHould be agitated");
                        selectedState = parentTransition.agitatedState;
                    }
                }
            }
            //Player isn't in direct sight to the enemy
            else
            {
                parentTransition.inDirectFOV = false;
                parentTransition.inDirectAttack = false;

            }

            return selectedState;
        }

        //public override State CheckTransition(Vector3 enemyPositon, Vector3 playerPosition)
        //{
        //    if (parentTransition.inFOV == false)
        //        return null;


        //    //ignores colliders on this specific layer
        //    LayerMask ignoreLayer = Physics.IgnoreRaycastLayer;
        //    Vector3 direction = (enemyPositon - playerPosition);
        //    RaycastHit hit;

        //    State selectedState = null;

        //    Ray ray = new Ray(enemyPositon + eyesOffset, -direction + directionOffset);

        //    if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreLayer))
        //    {
        //        //Player is in direct sight to the enemy
        //        if (hit.collider.tag == "Player")
        //        {
        //            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green, 1f);
        //            if (inAttack)
        //            {
        //                Debug.Log("attack");
        //                parentTransition.inDirectAttack = true;
        //                selectedState = parentTransition.attackState;
        //            }
        //            else
        //            {
        //                parentTransition.inDirectFOV = true;
        //                selectedState = parentTransition.chaseState;
        //            }

        //        }
        //        //Player isn't in direct sight to the enemy
        //        else
        //        {
        //            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 1f);
        //            parentTransition.inDirectFOV = false;
        //            parentTransition.inDirectAttack = false;

        //        }

        //        if (parentTransition.inDirectFOV == false && parentTransition.inFOV == true && inDirectFOV == false)
        //        {
        //            Debug.Log("SHould be agitated");
        //            selectedState = parentTransition.agitatedState;
        //        }
        //        //inFOV = parentTransition.inFOV; //true
        //        inDirectFOV = parentTransition.inDirectFOV;
        //    }
        //    return selectedState;
        //}
    }
}

