using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle
{
    public class NineTilePuzzle : Puzzle
    {
        [SerializeField] private GameObject emptySpace = null;

        private Camera camera; 
        // Start is called before the first frame update
        void Start()
        {
            camera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetButtonDown("Fire1"))
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition); 
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                Debug.DrawRay(ray.origin, ray.direction, Color.red, 5f);
                if(hit)
                {
                    Debug.Log(hit.transform.name);
                }


            }
        }
    }

}
