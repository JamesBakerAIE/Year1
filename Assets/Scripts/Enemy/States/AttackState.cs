using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers.UIManager;

namespace EnemyAI
{
    public class AttackState : State
    {
        private void Start()
        {
            //hearingCollider = GetComponentInChildren<SphereCollider>();

        }
        // Start is called before the first frame update
        public override void Enter()
        {
            FindObjectOfType<UIManager>().GameIsOver = true;
            if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<AudioSource>().clip != enemySound)
            {
                GameObject.FindGameObjectWithTag("Enemy").GetComponent<AudioSource>().clip = enemySound;
                GameObject.FindGameObjectWithTag("Enemy").GetComponent<AudioSource>().Play();
                GameObject.FindGameObjectWithTag("Enemy").GetComponent<AudioSource>().loop = false;
            }

            
            //hearingCollider.radius = hearingRadius;

        }
        public override void Exit()
        {

        }

        public override float GetSpeed()
        {
            return movementSpeed;
        }

        // Update is called once per frame
        //public override Vector3 UpdateAgent(Vector3 enemyPosition)
        //{
        //    return Vector3.zero;
        //}
    }
}
