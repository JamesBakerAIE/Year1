using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Transform camera;
    public Transform startArea;
    public Transform selectionArea;
    public Transform soundArea;
    public Transform resolutionArea;
    public Transform playArea;
    public Transform loadSceneArea;

    public GameObject pressAnyKeyText;


    public float cameraSpeed;
    // Start is called before the first frame update
    Transform destination;
    private void Start()
    {
        destination = startArea.transform;
    }
    // Update is called once per frame
    void Update()
    {
        camera.position = Vector3.MoveTowards(this.transform.position, destination.position, cameraSpeed * Time.deltaTime);
        camera.rotation = Quaternion.RotateTowards(this.transform.rotation, destination.rotation, cameraSpeed * 15 * Time.deltaTime);
        if (destination == startArea)
            if (Input.anyKey)
            {
                destination = selectionArea;
                Destroy(pressAnyKeyText);
            }

        if(destination == playArea && this.transform.position == playArea.transform.position)
        {
            destination = loadSceneArea;
        }

        if(this.transform.position == loadSceneArea.transform.position)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void SoundMenu(float speed)
    {
        cameraSpeed = speed;
        destination = soundArea;
    }

    public void ResolutionMenu(float speed)
    {
        cameraSpeed = speed;
        destination = resolutionArea;
    }

    public void MainMenu(float speed)
    {
        cameraSpeed = speed;
        destination = selectionArea;
    }

    public void Play(float speed)
    {
        cameraSpeed = speed;
        destination = playArea;
    }
}
