using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public virtual Vector3 UpdateAgent(Vector3 enemyPosition)
    {
        return Vector3.zero;
    }

}
