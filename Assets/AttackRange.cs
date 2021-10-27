using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EnemyAI
{

    public class AttackRange : MonoBehaviour
    {

        public SeenTransition seenTransition;
        private void Start()
        {
            seenTransition = GameObject.FindObjectOfType<SeenTransition>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Player")
                seenTransition.inAttack = true;

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
                seenTransition.inAttack = false;


        }
    }
}
