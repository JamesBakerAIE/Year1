using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tapeRecorderOff : MonoBehaviour
{
    public GameObject soundForward;
    public GameObject soundBackwards;
    public GameObject subtitles;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            soundForward.SetActive(false);
           
            subtitles.SetActive(false);
        }

    }

}