using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle
{
    public class KeyPuzzle : Puzzle
    {
        public float keyInsertSpeed = 1;
        public int insertedKeyCount = 0;
        public override void OnComplete()
        {
            base.OnComplete();
        }

        public override void UpdatePuzzle()
        {
            base.UpdatePuzzle();
            if(insertedKeyCount >= 2)
            {
                Debug.Log("Puzzle Compeleted");
            }

        }
    }
}

