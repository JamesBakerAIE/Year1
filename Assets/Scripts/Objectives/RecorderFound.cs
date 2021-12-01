using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers.UIManager;

public class RecorderFound : MonoBehaviour
{
    private UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.CodeFound();
        }
    }
}
