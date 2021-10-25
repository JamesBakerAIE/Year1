using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyAI;

public class FieldOfView : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            GameObject.FindObjectOfType<Enemy>().SeenPlayer();

    }
}
