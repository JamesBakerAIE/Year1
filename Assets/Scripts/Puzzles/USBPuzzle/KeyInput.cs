using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Puzzle
{
    public class KeyInput : KeyPuzzle
    {
        [SerializeField] private GameObject keyPrefab = null;
        private GameObject newKey = null;
        [SerializeField] private KeyPuzzle keyPuzzle = null;
        
        private GameObject[] keys = null;

        private List<bool> keysExists = new List<bool>();

        private Vector3 targetPosition = Vector3.zero;

        private PlayerController player = null;
      
        public void Awake()
        {
            keys = GameObject.FindGameObjectsWithTag("Key");
            player = FindObjectOfType<PlayerController>();
            foreach (var key in keys)
            {
                keysExists.Add(true);
            }
        }

        public void InsertKey()
        {
            if(newKey == null)
            {
                newKey = Instantiate(keyPrefab);
            }

            if(newKey.transform.position == targetPosition && newKey != null)
            {
                keyPuzzle.insertedKeyCount += 1;
                newKey = null;
            }
            else
            {
                newKey.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition, keyPuzzle.keyInsertSpeed);
            }


        }

        public void SetValues(Vector3 _targetPosition)
        {
            targetPosition = _targetPosition;
        }

        public override void UpdatePuzzle()
        {
            base.UpdatePuzzle();

            if (keysExists.Count >= 1 && player.keycardCount > 1)
            {
                InsertKey();
            }
        }
    }


}
