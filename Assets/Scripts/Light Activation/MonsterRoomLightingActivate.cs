using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRoomLightingActivate : MonoBehaviour
{
    public List<Light> lights = new List<Light>();

    public MonsterRoomLighting[] monsterRoomLighting;

    // Start is called before the first frame update
    void Start()
    {
        monsterRoomLighting = FindObjectsOfType<MonsterRoomLighting>();

        foreach (var item in monsterRoomLighting)
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
