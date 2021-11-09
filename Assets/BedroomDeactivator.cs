using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomDeactivator : MonoBehaviour
{
    public List<Light> lights = new List<Light>();

    public BedroomsLights[] bedroomsLights;

    // Start is called before the first frame update
    void Start()
    {
        bedroomsLights = FindObjectsOfType<BedroomsLights>();

        foreach (var item in bedroomsLights)
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