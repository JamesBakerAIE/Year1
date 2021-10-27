using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public class SeenTransition : Transition
    {
        public bool seenPlayer;
        public Vector3 eyesOffset;
        public Vector3 directionOffset;

        ChaseState chaseState;
        private void Start()
        {
            chaseState = GameObject.FindObjectOfType<ChaseState>();
        }

        public override State CheckTransition(Vector3 enemyPositon, Vector3 playerPosition /*Vector3 eyeOffset*/)
        {

            if (seenPlayer == false)
            {
                return new PatrolState();
            }
            LayerMask ignoreLayer = Physics.IgnoreRaycastLayer;
            Vector3 direction = (enemyPositon - playerPosition);
            RaycastHit hit;

            Ray ray = new Ray(enemyPositon + eyesOffset, -direction + directionOffset);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreLayer))
            {
                //Debug.DrawRay(this.transform.position, Vector3.forward, Color.red)
                if (hit.collider.tag == "Player")
                {
                    //Debug.DrawRay(this.transform.position + eyesOffset, -direction + directionOffset, Color.green, 1f);
                    Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green, 1f);
                    return chaseState;
                }
                else
                {
                    Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 1f);
                }
            }
            return null;
        }
    }
}

