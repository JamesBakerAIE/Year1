using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyAI;

namespace EnemyAI
{
    public class FieldOfView : MonoBehaviour
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
                transition.inFOV = true;

            }

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                transition.inFOV = false;
            }


        }
    }
}
