using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DerbisSpawner : MonoBehaviour
{
    public GameObject[] Debris;
    public GameObject SUN, henryHoover;
    public int debrisMaxRange = 1000, debrisMaxDistance = 1000;

    // Start is called before the first frame update
    void Start()
    {
        int debrisCount = Random.Range(debrisMaxRange / 2, debrisMaxRange);

        for (int i = 0; i < debrisCount; i++)
        {
            GameObject temp = Instantiate(Debris[Random.Range(0, Debris.Length)], transform);
            temp.GetComponent<OrbitFunction>().SUN = SUN;
            temp.GetComponent<debrisFunction>().debrisRange = debrisMaxDistance;
            temp.GetComponent<debrisFunction>().CallAwake();
        }

        henryHoover.GetComponent<HenryController>().startDebris = debrisCount;
        henryHoover.GetComponent<HenryController>().curDebris = debrisCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
