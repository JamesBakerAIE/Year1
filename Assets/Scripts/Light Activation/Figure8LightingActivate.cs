using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure8LightingActivate : MonoBehaviour
{
    public List<Light> lights = new List<Light>();

    public Figure8Lighting[] figure8Lighting;

    // Start is called before the first frame update
    void Start()
    {
        figure8Lighting = FindObjectsOfType<Figure8Lighting>();

        foreach (var item in figure8Lighting)
        {
            lights.Add(item.GetComponent<Light>());
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var item in lights)
            {
                item.gameObject.SetActive(isActiveAndEnabled);
            }


        }
    }
}
