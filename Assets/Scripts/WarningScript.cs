using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarningScript : MonoBehaviour
{
   
    void Start()
    {
        StartCoroutine(loadSceneAfterDelay(8));

    }

    IEnumerator loadSceneAfterDelay(float waitbySecs)
    {

        yield return new WaitForSeconds(waitbySecs);
        SceneManager.LoadScene(1);
    }
}
  


