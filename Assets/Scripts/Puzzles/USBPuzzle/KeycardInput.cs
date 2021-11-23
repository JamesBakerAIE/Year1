using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Puzzle
{
    public class KeycardInput : Puzzle
    {
        public GameObject keycardPrefab;
        private GameObject keycardNew;

        private KeycardPuzzle keycardPuzzle = null;

        private PlayerController player;

        private void Start()
        {
            keycardPuzzle = FindObjectOfType<KeycardPuzzle>();
            player = FindObjectOfType<PlayerController>();
        }
        public GameObject keyPositionObject;

        public GameObject door = null;

        // Update is called once per frame
        void Update()
        {
            if (keycardPuzzle.keycardInsertedCount >= 2 && door.activeInHierarchy)
            {
                door.SetActive(false);
            }
        }

        public void SpawnCard()
        {
            Vector3 pos = new Vector3(keyPositionObject.transform.position.x, keyPositionObject.transform.position.y, keyPositionObject.transform.position.z);

            keycardNew = Instantiate(keycardPrefab, pos, keyPositionObject.transform.rotation);
            keycardPuzzle.keycardInsertedCount++;
            player.keycardCount -= 1;
        }
    }

}
