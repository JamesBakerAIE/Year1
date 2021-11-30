using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLightTest : MonoBehaviour
{
    public bool isflickering = false;
    private Light light;
    public float timedelay;

    private void OnDisable()
    {
    }
    void Update()
    {
        if (isflickering == false)
        {
            StartCoroutine(flickeringlight());
        }
    }
    public IEnumerator flickeringlight()
    {

        yield return new WaitForSeconds(Random.Range(0.01f, 5f));
        if(light == null)
        {
            light = gameObject.GetComponent<Light>();
        }

        light.enabled = !light.enabled;


        //isflickering = true;
        //this.gameObject.GetComponent<Light>().enabled = false;
        //timedelay = Random.Range(0.01f, 0.3f);
        //yield return new WaitForSeconds(timedelay);
        //this.gameObject.GetComponent<Light>().enabled = true;
        //timedelay = Random.Range(0.01f, 0.3f);
        //yield return new WaitForSeconds(timedelay);
        //isflickering = false;
    }
}
