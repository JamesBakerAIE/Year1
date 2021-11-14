using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{


    public class HideSpot : MonoBehaviour
    {
        public float searchChance;
        public bool searched = false;
        public bool hasPlayer = false;
        public Renderer doorObject;

        private void Start()
        {
            foreach(Transform gameObject in this.GetComponentInChildren<Transform>())
            {
                if (gameObject.tag == "Locker")
                    doorObject = gameObject.GetComponent<Renderer>();
            }
        }
    }

}
