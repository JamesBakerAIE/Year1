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
        public GameObject enemy;
        public Animator attackAnimator;
        //Locker

        private void Start()
        {
            foreach(Transform gameObject in this.GetComponentInChildren<Transform>())
            {
                if (gameObject.tag == "Locker")
                    doorObject = gameObject.GetComponent<Renderer>();
            }

            //enemy.SetActive(false);
        }

        public void AnimateAttack()
        {
            enemy.SetActive(true);
            attackAnimator.SetBool("IsSearching", true);
        }
    }

}
