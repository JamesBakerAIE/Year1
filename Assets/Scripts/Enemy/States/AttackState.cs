using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EnemyAI
{
    public class AttackState : State
    {
        GameObject deathMenu;
        GameObject pauseMenu;
        // Start is called before the first frame update
        public override void Enter()
        {
            Debug.Log("Entered attack state");
            deathMenu.SetActive(true);
            pauseMenu.SetActive(false);
        }

        private void Start()
        {
            deathMenu = GameObject.FindGameObjectWithTag("Finish");
            deathMenu.SetActive(false);

            pauseMenu = GameObject.FindGameObjectWithTag("Start");
            pauseMenu.SetActive(false);

        }
        // Update is called once per frame
        //public override Vector3 UpdateAgent(Vector3 enemyPosition)
        //{
        //    return Vector3.zero;
        //}
    }
}
