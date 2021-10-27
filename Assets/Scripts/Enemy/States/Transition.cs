using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public class Transition : MonoBehaviour
    {
        public virtual State CheckTransition(Vector3 enemyPositon, Vector3 playerPosition/*, Vector3 eyeOffset*/)
        {
            return null;
        }
    }
}
