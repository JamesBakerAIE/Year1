using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRoomLightingDeactivation : MonoBehaviour
{
    public List<Light> lights = new List<Light>();

    public GRoomLighting[] gRoomLighting;

    // Start is called before the first frame update
    void Start()
    {
        gRoomLighting = FindObjectsOfType<GRoomLighting>();

        foreach (var item in gRoomLighting)
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
                item.gameObject.SetActive(false);
            }


        }
    }
}