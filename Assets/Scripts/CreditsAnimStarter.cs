using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsAnimStarter : MonoBehaviour
{
    public float delay = 0.0f;

    public GameObject names;
    public GameObject title;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayAnim());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlayAnim()
    {
        yield return new WaitForSeconds(delay);
        names.SetActive(true);
        title.SetActive(true);
        source.enabled = true;
    }
}
