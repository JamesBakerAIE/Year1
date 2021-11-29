using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLightTest : MonoBehaviour
{
    public bool isflickering = false;
    public float timedelay;

    private void ondisable()
    {
    }
    void update()
    {
        if (isflickering == false)
        {
            StartCoroutine(flickeringlight());
        }
    }
    IEnumerator flickeringlight()
    {
        isflickering = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        timedelay = Random.Range(0.01f, 0.3f);
        yield return new WaitForSeconds(timedelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timedelay = Random.Range(0.01f, 0.3f);
        yield return new WaitForSeconds(timedelay);
        isflickering = false;
    }
}
