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

        public GameObject keyPositionObject;

        PlayerController player;
        // Start is called before the first frame update
        void Start()
        {
            player = FindObjectOfType<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SpawnCard()
        {
            Vector3 pos = new Vector3(keyPositionObject.transform.position.x, keyPositionObject.transform.position.y, keyPositionObject.transform.position.z);

            keycardNew = Instantiate(keycardPrefab, pos, keyPositionObject.transform.rotation);

            Debug.Log("Test");
        }
    }

}
