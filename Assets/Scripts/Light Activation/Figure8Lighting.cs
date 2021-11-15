using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure8Lighting : MonoBehaviour
{
    public GameObject lightOn;
    public GameObject lightOff;

    public List<Light> lights = new List<Light>();
    public Light[] temp;

    public Figure8Lighting[] figure8Lighting;

    // Start is called before the first frame update
    void Start()
    {



        //temp = FindObjectsOfType<Light>();

        //foreach (var item in temp)
        //{
        //    lights.Add(item);
        //}
        //Debug.Log(lights);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
