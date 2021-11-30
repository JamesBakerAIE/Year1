using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers.UIManager;

public class clickControl : MonoBehaviour
{
    public static string correctCode = "4198";
    public static string playerCode = "";
    public GameObject door;
    public GameObject door2;
    public GameObject hitBox;
    public GameObject hitBox1;
    public GameObject computerPuzzle;
    public GameObject computerPrefab;
    public GameObject endGame;

    public static string didclick = "n";

    public static int totalDigits = 0;
    private bool complete;

    private UIManager uiManager;
    private AllPuzzlesDone puzzlesDone;


    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        puzzlesDone = FindObjectOfType<AllPuzzlesDone>();

    }

    // Update is called once per frame
    void Update()
    {
        if (totalDigits == 4)
        {
            if (playerCode == correctCode)
            {
                complete = true;
                door.SetActive(false);
                door2.SetActive(false);
                Debug.Log("Correct");
                hitBox.SetActive(true);
                hitBox1.SetActive(true);
                computerPuzzle.SetActive(false);
                computerPrefab.SetActive(true);
                //endGame.SetActive(true);


                uiManager.ChangeEggText();
                puzzlesDone.codePuzzleDone = true;

            }
            else
            {
                playerCode = "";
                totalDigits = 0;
                Debug.Log("wrong code");
            }
        }
    }
    private void OnMouseUp()
    {
        if (!complete)
        {
            playerCode += gameObject.name;
            totalDigits += 1;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 0);
            StartCoroutine(waittochange());
            didclick = "y";
        }

    }
    private void OnMouseOver()
    {
        if (!complete)
        {
            if (didclick == "n")
                Debug.Log("Hovering");
            GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
        }
    }
    private void OnMouseExit()
    {
        if (didclick == "n")
            Debug.Log("Not Hovering");
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        didclick = "n";
    }
    IEnumerator waittochange()
    {
        yield return new WaitForSeconds(1);
    }
}

