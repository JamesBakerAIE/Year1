using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMap : MonoBehaviour
{
    public GameObject on;
    public GameObject off;
    public GameObject m;
    
    private bool isOn;
    // Start is called before the first frame update
    void Start()
    {
        on.SetActive(false);
        off.SetActive(false);
        isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (isOn)
            {
                on.SetActive(false);
                off.SetActive(true);
            }

            if (!isOn)
            {
                on.SetActive(true);
                off.SetActive(false);
            }
            m.SetActive(false);
        }
        isOn = !isOn;
    }
}
  
