using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyAI;

namespace EnemyAI
{
    public class FieldOfView : MonoBehaviour
    {
        public Enemy enemy;

        private void Start()
        {
            enemy = GameObject.FindObjectOfType<Enemy>();
        }

        private void OnTriggerStay(Collider other)
        { 
            if (other.gameObject.tag == "Player")
                enemy.SeenPlayer(other.gameObject.transform);

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
                enemy.SeenPlayer(other.gameObject.transform);

        }
    }
}
