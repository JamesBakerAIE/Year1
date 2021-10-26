using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EnemyAI
{

    public class AttackRange : MonoBehaviour
    {
        public Enemy enemy;

        private void Start()
        {
            enemy = GameObject.FindObjectOfType<Enemy>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {

                enemy.AttackPlayer();

            }
        }
    }
}
