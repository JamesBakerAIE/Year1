using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Go_to_Beggining_Scene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GOTOMainMenu()
    {
        SceneManager.LoadScene("Test_1");
        Debug.Log("Going to Main menu");
    }
}

