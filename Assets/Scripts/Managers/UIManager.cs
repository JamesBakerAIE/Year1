using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

namespace Managers.UIManager
{
    public class UIManager : MonoBehaviour
    {
        //PauseMenu
        public static bool GameIsPaused = false;
        public GameObject pauseMenuUI;

        //OptionsMenu
        public GameObject optionsMenuUI;

        // MainMenu Scene
        [SerializeField] private string mainMenuSceneName = string.Empty;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Pause))
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

        public void BackButton()
        {
            optionsMenuUI.SetActive(false);
            pauseMenuUI.SetActive(true);
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadSceneAsync(mainMenuSceneName);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

    }

}
