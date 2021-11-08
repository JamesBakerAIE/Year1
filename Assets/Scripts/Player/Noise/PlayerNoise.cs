using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyAI;

namespace Noise
{
    public class PlayerNoise : MonoBehaviour
    {
        [Header("Player Noise")]
        [SerializeField] private SphereCollider noiseCollider = null;
        [SerializeField] private float noiseRadius = 0f;
        [SerializeField] private LayerMask wallsMask;
        private GameObject enemy = null;
        private ChaseState chaseState = null;
        private StateMachine stateMachine = null;


        public void Start()
        {

        }

        private void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            //if(other.CompareTag("Enemy"))
            //{
            //    Debug.Log(other.gameObject.name);
            //    chaseState = enemy.GetComponent<ChaseState>();
            //    stateMachine.CurrentState = chaseState;
            //}


            //Vector3 direction = other.transform.position - transform.position;

            //direction.Normalize();

            //Ray ray = new Ray(transform.position, direction);
            //RaycastHit hit;
            //if(Physics.Raycast(ray, out hit, noiseRadius, wallsMask))
            //{
            //    Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, Mathf.Infinity);
            //}


        }

    }

}
