using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitFunction : MonoBehaviour
{
    public bool randomPlane = true;

    public GameObject SUN;

    public float Radius = 100, radiusSpeed = .5f, rotationSpeed = 80, orbitOffset = 0;

    public Vector3 orbitPlane = Vector3.zero;

    float[] randomOffsets;

    // Start is called before the first frame update
    void Start()
    {
        Radius = Vector3.Distance(SUN.transform.position, transform.position);
        rotationSpeed = 1000 / Radius;
       
        if (randomPlane)
            orbitOffset = Random.value * 5 - 2.5f;

        orbitPlane = new Vector3(0, 1, orbitOffset).normalized;


        transform.position = new Vector3(Radius, 0, 0);

       

        transform.RotateAround(SUN.transform.position, orbitPlane, Random.Range(0, 500));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SUN = GameObject.FindGameObjectWithTag("Sun");

        orbitPlane = new Vector3(0, 1, orbitOffset).normalized;

        transform.RotateAround(SUN.transform.position, orbitPlane, rotationSpeed * Time.deltaTime);

        var desiredPosition = (transform.position - SUN.transform.position).normalized * Radius + SUN.transform.position;

        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);


    }
}
