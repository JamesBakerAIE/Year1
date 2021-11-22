using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightAlert : MonoBehaviour
{
    public GameObject lightOn;
    public GameObject lightOff;
    public int flickerChance;
    private Light myLight;
    public float flicker;
    public GameObject lightFlicker;
    public GameObject sound;
    
    
    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        //sound.SetActive(false);
      
    }

    // Update is called once per frame
    void Update()
    {
        //float distanceFromEnemy = Vectir3Distance(playerttrnasofmrpositon, enemytransofmposition) (1f, 
      //  float dtef = Vector3.Distance(this.transform.position, )
        //if distandc from enemy < 10
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lightOn.GetComponent<FlashLight>().isFlickering = true;
            lightFlicker.SetActive(true);
            sound.SetActive(true);

        }
      




    }



    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lightOn.GetComponent<FlashLight>().isFlickering = false;
            lightFlicker.SetActive(false);
            sound.SetActive(false);

        }
    }
}
