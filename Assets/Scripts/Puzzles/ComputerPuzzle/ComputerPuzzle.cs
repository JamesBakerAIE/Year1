using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Puzzle
{
    public class ComputerPuzzle : Puzzle
    {
        [SerializeField] private GameObject door;

        private Slider slider;
        private AllPuzzlesDone puzzlesDone;

        // Start is called before the first frame update
        void Start()
        {
            slider = GetComponentInChildren<Slider>();
            puzzlesDone = FindObjectOfType<AllPuzzlesDone>();
        }

        // Update is called once per frame
        void Update()
        {
            if(slider.value == slider.maxValue)
            {
                OnComplete();
            }
        }

        public override void OnComplete()
        {
            door.SetActive(false);
            puzzlesDone.computerPuzzleDone = true;
        }
    }

}
