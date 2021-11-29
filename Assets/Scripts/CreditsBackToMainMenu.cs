using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsBackToMainMenu : MonoBehaviour
{
    public void ReturnToMenuAfterCredits()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Sprint 3 Main Menu");
    }
}
