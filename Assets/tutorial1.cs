using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial1 : MonoBehaviour
{
    public GameObject mouse;
    public GameObject w;

    // Start is called before the first frame update
    void Start()
    {
        w.SetActive(true);
        mouse.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            w.SetActive(false);
            mouse.SetActive(false);
        }

    }
}
