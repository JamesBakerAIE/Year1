using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyAI;

namespace EnemyAI
{
    public class FieldOfView : MonoBehaviour
    {
        public Enemy enemy;
        public SeenTransition seenTransition;

        private void Start()
        {
            enemy = GameObject.FindObjectOfType<Enemy>();
            seenTransition = GameObject.FindObjectOfType<SeenTransition>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Player")
                seenTransition.seenPlayer = true;

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
                seenTransition.seenPlayer = true;

        }
    }
}
