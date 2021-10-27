using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public class Path : MonoBehaviour
    {
        public List<GameObject> waypoints;

        private void Start()
        {
            GameObject.FindGameObjectsWithTag("Waypoints");
        }
    }
}
