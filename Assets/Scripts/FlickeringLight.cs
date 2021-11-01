using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public int flickerChance;
    private Light myLight;
    public float flicker;
    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
    }
    // Update is called once per frame
    void Update()
    {

        flicker = Random.Range(0, 100000f);
        if (flicker < flickerChance)
        {
            myLight.enabled = false;
            Debug.Log("Off");
        }
        else
        {
            myLight.enabled = true;
            Debug.Log("On");

        }
    }


}
