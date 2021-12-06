using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EnemyAI
{

    public class AttackRange : MonoBehaviour
    {

        Transition transition;

        private void Start()
        {
            transition = GameObject.FindObjectOfType<Transition>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                transition.inAttack = true;
            }

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                transition.inAttack = false;
            }
        }
    }
}
