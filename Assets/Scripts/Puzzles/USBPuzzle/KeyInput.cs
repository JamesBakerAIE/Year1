using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle
{
    public class KeyInput : KeyPuzzle
    {
        [SerializeField] private GameObject keyPrefab = null;
        public void InsertKey()
        {
            Transform keyTransform = keyPrefab.transform;

            //Instantiate(keyPrefab,);
        }
    }


}
