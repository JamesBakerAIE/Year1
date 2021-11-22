using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeRecorder : MonoBehaviour
{
    public GameObject soundForward;
    public GameObject soundBackwards;
    public GameObject subtitles;
    public GameObject Beacon;
    // Start is called before the first frame update
    void Start()
    {
        soundForward.SetActive(false);
        //soundBackwards.SetActive(false);
        subtitles.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if  (Input.GetKey(KeyCode.Mouse0))
        {
         
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            soundForward.SetActive(true);
            Beacon.SetActive(false);

            subtitles.SetActive(true);
        }
            
    }

}

