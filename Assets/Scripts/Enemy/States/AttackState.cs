﻿using System.Collections;
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
            if (enemyAudio.clip != enemySound)
            {
                enemyAudio.clip = enemySound;
                enemyAudio.Play();
                enemyAudio.loop = false;
            }

            //delete after sounds and everything have happened
            if (searchState.foundPlayer)
            {
                Destroy(this.gameObject);
            }

            Debug.Log("Player is dead");
            isAttacking = true;
            //hearingCollider.radius = hearingRadius;

        }

        public override Vector3 RotationUpdate()
        {
            return playerPosition.position;
 
        }
        public override void Exit()
        {

        }

        public override float GetSpeed()
        {
            return movementSpeed;
        }
    }
}
