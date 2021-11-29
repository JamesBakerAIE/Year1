using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers.UIManager;

public class AllPuzzlesDone : MonoBehaviour
{
    public bool keycardPuzzleDone = false;
    public bool computerPuzzleDone = false;
    public bool codePuzzleDone = false;

    private UIManager uiManager;


    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if(keycardPuzzleDone && computerPuzzleDone && codePuzzleDone)
        {
            uiManager.DockingDayDoorOpened();
        }
    }
}
