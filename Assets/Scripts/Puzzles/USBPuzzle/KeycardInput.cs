﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Managers.UIManager;

namespace Puzzle
{
    public class KeycardInput : Puzzle
    {
        public GameObject keycardPrefab;
        private GameObject keycardNew;

        private KeycardPuzzle keycardPuzzle = null;

        private PlayerController player;


        public GameObject keyPositionObject;

        public GameObject door = null;

        private bool hasBeenInserted = false;

        public UIManager uiManager;
        private AllPuzzlesDone puzzlesDone;

        private void Start()
        {
            keycardPuzzle = FindObjectOfType<KeycardPuzzle>();
            player = FindObjectOfType<PlayerController>();
            uiManager = FindObjectOfType<UIManager>();
            puzzlesDone = FindObjectOfType<AllPuzzlesDone>();
        }


        // Update is called once per frame
        void Update()
        {
            if (keycardPuzzle.keycardInsertedCount >= 2 && door.activeInHierarchy)
            {
                door.SetActive(false);
                uiManager.ChangeAccessKeyText();
                puzzlesDone.keycardPuzzleDone = true;
            }
        }

        public void SpawnCard()
        {
            // Insert card into the keycard input
            if(hasBeenInserted == false)
            {
                Vector3 pos = new Vector3(keyPositionObject.transform.position.x, keyPositionObject.transform.position.y, keyPositionObject.transform.position.z);

                keycardNew = Instantiate(keycardPrefab, pos, keyPositionObject.transform.rotation);
                keycardPuzzle.keycardInsertedCount++;
                player.keycardCount -= 1;
                hasBeenInserted = true;
            }

        }
    }

}
