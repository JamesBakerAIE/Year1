using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLightEvent : MonoBehaviour
{
    [SerializeField] private AudioSource breakSound;
    [SerializeField] private AudioSource squeakSound;
    [SerializeField] private AudioClip breakSoundClip;
    [SerializeField] private GameObject soundArea;
    [SerializeField] private Camera camera;

    private bool isExecuting = false;
    private bool readyToRotate = false;


    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        //breakSoundClip = breakSound.
    }

    // Update is called once per frame
    void Update()
    {
        if(isExecuting == true && readyToRotate == true)
        {
            camera.transform.LookAt(soundArea.transform);
        }
    }

    public void StartEvent()
    {
        isExecuting = true;

        breakSound.Play();
        StartCoroutine(WaitForBreakClipTime());
        
    }

    public IEnumerator WaitForBreakClipTime()
    {
        yield return new WaitForSeconds(breakSoundClip.length);
        readyToRotate = true;
        squeakSound.Play();
    }
}
