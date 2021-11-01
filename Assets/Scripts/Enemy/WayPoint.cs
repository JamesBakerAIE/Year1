using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{


    public class WayPoint : MonoBehaviour
    {
        public float wayPointChance;
        public bool patrolled = false;

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.gameObject.tag == "Enemy")
        //    {
        //        patrolled = true;
        //        FindObjectOfType<PatrolState>().CheckPath();
        //    }
        //}
    }
}
