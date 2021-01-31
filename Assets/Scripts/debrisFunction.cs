using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debrisFunction : MonoBehaviour
{

    public int debrisRange = 1000;

    // Start is called before the first frame update
    public void CallAwake()
    {
        GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(0, 100), Random.Range(0, 100), Random.Range(0, 100));
        transform.position = new Vector3(Random.Range(0, debrisRange), 0, 0);
        this.tag = "Debris";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
