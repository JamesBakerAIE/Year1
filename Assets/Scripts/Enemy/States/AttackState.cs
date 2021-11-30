using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers.UIManager;
using Player;

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
            //NONE OF THIS WILL WORK WITH THE LOCKER ATTACK
            if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<AudioSource>().clip != enemySound)
            {
                GameObject.FindGameObjectWithTag("Enemy").GetComponent<AudioSource>().clip = enemySound;
                GameObject.FindGameObjectWithTag("Enemy").GetComponent<AudioSource>().Play();
                GameObject.FindGameObjectWithTag("Enemy").GetComponent<AudioSource>().loop = false;
            }

            //delete after sounds and everything have happened
            if (FindObjectOfType<SearchState>().hasSniffed == true)
            {
                Destroy(this.gameObject);
            }

            Debug.Log("Player is dead");
            isAttacking = true;
            //hearingCollider.radius = hearingRadius;

        }

        public override Vector3 RotationUpdate()
        {
            Vector3 playerPosition = FindObjectOfType<PlayerController>().transform.position;
            return playerPosition;
 
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
