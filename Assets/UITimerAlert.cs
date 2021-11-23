using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITimerAlert : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update

    void Start()
    {
        StartCoroutine(WaitBeforeShow());
       
        


       // Task.delay(5000);
       // System.Threading.Thread.Sleep(5000);
     
    }

    // Update is called once per frame
    

    IEnumerator WaitBeforeShow()
    {
        yield return new WaitForSeconds(17);
        anim.SetBool("Play", true);

      


        Debug.Log("Working");
        
      
    }
    void Update()
    {
        //anim.Play("Time");
    }

}
