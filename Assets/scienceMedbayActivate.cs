using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scienceMedbayActivate : MonoBehaviour
{
    public List<Light> lights = new List<Light>();

    public ScienceMedbayLight[] scienceMedbayLight;

    // Start is called before the first frame update
    void Start()
    {
        scienceMedbayLight = FindObjectsOfType<ScienceMedbayLight>();

        foreach (var item in scienceMedbayLight)
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
