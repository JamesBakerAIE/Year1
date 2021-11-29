using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

using UnityEngine;

namespace Managers.UIManager
{
    public class UIManager : MonoBehaviour
    {
        public Camera mainCamera;
        //PauseMenu
        public static bool GameIsPaused = false;
        public bool GameIsOver = false;
        public GameObject pauseMenuUI;
        public GameObject deathMenu;

        //OptionsMenu
        public GameObject optionsMenuUI;
       
        //Main Menu
        public GameObject mainMenu;

        // MainMenu Scene
        [SerializeField] private string mainMenuSceneName = string.Empty;
        [SerializeField] private string mainGameSceneName = string.Empty;

        public float timeAgitated = 5;
        public float timeElapsed = 0;
        public float FOVZoom = 90;


        Resolution[] resolutions;
        public Dropdown resolutionDropdown;

        public void Start()
        {
            if(resolutionDropdown != null)
            {
                resolutions = Screen.resolutions;
                resolutionDropdown.ClearOptions();
                List<string> options = new List<string>();
                int currentResolutionIndex = 0;
                for (int i = 0; i < resolutions.Length; i++)
                {
                    string option = resolutions[i].width + " x " + resolutions[i].height;
                    options.Add(option);
                    if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                    {
                        currentResolutionIndex = i;
                    }
                }
                resolutionDropdown.AddOptions(options);
                resolutionDropdown.value = currentResolutionIndex;
                resolutionDropdown.RefreshShownValue();
            }



        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        }

        string ResToString(Resolution res)
        {
            return res.width + " x " + res.height;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Pause))
            {
                if (GameIsOver == false)
                {
                    if (GameIsPaused)
                    {
                        Resume();
                        Cursor.lockState = CursorLockMode.Locked;
                    }
                    else
                    {
                        Pause();
                        Cursor.lockState = CursorLockMode.Confined;
                    }
                }
            }

            if(GameIsOver == true)
            {
                if (timeElapsed >= timeAgitated)
                {
                    timeElapsed = 0;
                    Death();
                }
                else
                {
                    timeElapsed += Time.deltaTime;
                }

                if (FOVZoom > 60 && timeElapsed > timeAgitated / 2)
                {
                    FOVZoom -= timeElapsed * 5;
                    mainCamera.fieldOfView = FOVZoom;
                }


            }

        }
        public void Resume()
        {
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }

        void Pause()
        {
            Cursor.lockState = CursorLockMode.Confined;
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }

        public void OptionsMenu()
        {
            pauseMenuUI.SetActive(false);
            optionsMenuUI.SetActive(true);
        }

        public void MainMenuOptionsMenu()
        {
            mainMenu.SetActive(false);
            optionsMenuUI.SetActive(true);
        }


        public void BackButton()
        {
            optionsMenuUI.SetActive(false);
            pauseMenuUI.SetActive(true);
        }
        public void MainMenuBackButton()
        {
            mainMenu.SetActive(true);
            optionsMenuUI.SetActive(false);
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadSceneAsync(mainMenuSceneName);
        }

        public void PlayGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadSceneAsync(mainGameSceneName);
        }

        public void Death()
        {
            optionsMenuUI.SetActive(false);
            deathMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            //Time.timeScale = 0;
            mainGameSceneName = SceneManager.GetActiveScene().name;
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
  


}