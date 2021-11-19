using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EnemyAI 
{ 
    public class HearingRange : MonoBehaviour
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
                FindObjectOfType<SearchState>().foundPlayer = true;
                transition.inHearingRange = true;
            }

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                FindObjectOfType<SearchState>().foundPlayer = false;
                transition.inHearingRange = false;
            }
        }
    }
}
