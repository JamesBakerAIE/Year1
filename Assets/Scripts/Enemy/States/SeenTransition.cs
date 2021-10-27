using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public class SeenTransition : Transition
    {
        public bool inFOV = false;
        public bool inAttack = false;
        public Vector3 eyesOffset;
        public Vector3 directionOffset;

        State changeState;
        ChaseState chaseState;
        PatrolState patrolState;
        AttackState attackState;
        private void Start()
        {
            chaseState = GameObject.FindObjectOfType<ChaseState>();
            patrolState = GameObject.FindObjectOfType<PatrolState>();
            attackState = GameObject.FindObjectOfType<AttackState>();
        }

        public override State CheckTransition(Vector3 enemyPositon, Vector3 playerPosition)
        {

            if (inFOV == false)
                return null;

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
                        return attackState;
                    else
                        return chaseState;
                }
                //Player isn't in direct sight to the enemy
                else
                {
                    Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 1f);
                    return patrolState;

                }
            }
            return null;
        }
    }
}

