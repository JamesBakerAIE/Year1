using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GasTimer
{
    public class IncreaseGas : MonoBehaviour
    {

        public float density = 0.0f;

        private float timerDuration = 3f * 60f;
        private float timer;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                //UpdateTimerDisplay(timer);
            }
        }

        public void IncreaseDensity(float time)
        {

        }
    }
}
