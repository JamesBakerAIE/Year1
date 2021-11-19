﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public class SeenTransition : Transition
    {
        //public Vector3 eyesOffset;
        //public Vector3 directionOffset;
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

        State selectedState;
        readonly List<Ray> rays;
        public override State CheckTransition(Vector3 enemyPositon, Vector3 playerPosition)
        {
            inHearingRange = parentTransition.inHearingRange;
            bool wasInFOV = parentTransition.inDirectFOV;

            Vector3 direction = (GameObject.FindGameObjectWithTag("Eyes").transform.position - playerPosition);
            selectedState = null;

            //an array that shoots in the player's direction

            Vector3 rayStart = GameObject.FindGameObjectWithTag("Eyes").transform.position;
            Vector3 rayDestination = direction;

            bool hitPlayer = false;

            if (hitPlayer == false)
                hitPlayer = ShootRay(rayStart, rayDestination);
            if (hitPlayer == false)
                hitPlayer = ShootRay(rayStart, rayDestination + new Vector3(0, 0, 1));
            if (hitPlayer == false)
                hitPlayer = ShootRay(rayStart, rayDestination + new Vector3(0, 0, -1));



            //Player isn;t in direct sight of the enemy
            if (hitPlayer == false)
            {
                //Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 1f);

                parentTransition.inDirectFOV = false;
            }

            //Last time this was called it was in field of view, now it isn't so become agitated
            if (parentTransition.inDirectFOV == false && wasInFOV == true || inHearingRange == true)
            {
                parentTransition.inDirectFOV = false;
                parentTransition.inDirectAttack = false;
                selectedState = parentTransition.agitatedState;

            }

            //update previous FOV
            wasInFOV = parentTransition.inDirectFOV;

            return selectedState;
        }

        bool ShootRay(Vector3 enemyPosition, Vector3 direction)
        {
            Ray ray = new Ray(enemyPosition, -direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreLayer))
            {
                //Player is in direct sight to the enemy
                if (hit.collider.tag == "Player" && parentTransition.inFOV)
                {

                    //In attack range
                    Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green, 1f);
                    if (parentTransition.inAttack)
                    {
                        parentTransition.inDirectAttack = true;
                        parentTransition.inDirectFOV = true;
                        selectedState = parentTransition.attackState;
                        return true;
                    }
                    //In view range
                    else if (parentTransition.inFOV)
                    {
                        parentTransition.inDirectFOV = true;
                        selectedState = parentTransition.chaseState;
                        return true;

                    }

                }
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 1f);
                //Debug.Log(hit.collider.name);
            }
            return false;

        }

    }
}

