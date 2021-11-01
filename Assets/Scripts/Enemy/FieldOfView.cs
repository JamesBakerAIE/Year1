using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyAI;

namespace EnemyAI
{
    public class FieldOfView : MonoBehaviour
    {
        SeenTransition seenTransition;

        private void Start()
        {
            seenTransition = GameObject.FindObjectOfType<SeenTransition>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Player")
                seenTransition.inFOV = true;

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
                seenTransition.inFOV = false;


        }
    }
}
