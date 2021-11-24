using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuEvent : MonoBehaviour
{
    private Rigidbody rigidbody;
    public Vector3 force;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartEvent()
    {
        rigidbody.AddForce(force, ForceMode.Impulse);
    }

}
