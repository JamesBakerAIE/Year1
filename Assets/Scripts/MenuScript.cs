using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScript : MonoBehaviour
{

    private static int lastScene;
    private int currentScene;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene("Options");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
