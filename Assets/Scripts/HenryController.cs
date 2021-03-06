﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HenryController : MonoBehaviour
{
    public float throttleForce = 1, direactionalBrakingForce = 1, angularBrakingForce = 5, angularThrustForce = 5;

    public float angularConstraint = 10, curThrust = 0, maxThrust = 10;

    public GameObject hoseEnd, hoseCollider;

    public Scrollbar throttleUI;
    public Text speedText;
    public Slider progressSlider;

    public int throttlePos = 0, startDebris, curDebris;



    public float debugVal = 0;
    public GameObject debugObject;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        hoseEnd = this.gameObject;
        hoseCollider = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere));
        hoseCollider.AddComponent<DebrisSucker>();
        hoseCollider.transform.localScale = new Vector3(180, 180, 180);
        hoseCollider.transform.parent = hoseEnd.transform;
        hoseCollider.transform.position = hoseEnd.transform.position;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.tag == "Debris")
    //    {
    //        curDebris--;
    //        Destroy(collision.gameObject);
    //    }
    //}

    void FixedUpdate()
    {
        


        Rigidbody rb = GetComponent<Rigidbody>();

        rb.AddTorque(transform.right * Input.GetAxis("Mouse Y") * angularThrustForce);
        rb.AddTorque(transform.forward * Input.GetAxis("Mouse X") * -angularThrustForce);

        rb.AddForce(transform.right * Input.GetAxis("Horizontal") * throttleForce);
        rb.AddForce(transform.up * Input.GetAxis("Uppy2") * throttleForce);
        rb.AddForce(transform.forward* Input.GetAxis("Vertical") * throttleForce);
        rb.AddTorque(transform.up * Input.GetAxis("Rotational") * -angularThrustForce);
        
        rb.AddForce(transform.forward * curThrust);

        throttleUI.value = (curThrust / maxThrust) / 2 + .5f;

        throttleContraints();

        if (Input.GetKeyUp("left ctrl") || Input.GetKeyUp("left shift"))
        {
            if (curThrust == 0)
                throttlePos = 0;
        }

        if (Input.GetKey("space"))
        {
            rb.drag = direactionalBrakingForce;
            rb.angularDrag = angularBrakingForce;
        }
        else
        {
            rb.drag = 0;
            rb.angularDrag = 0.1f;
        }

        speedText.text = "Speeeed: \n " + rb.velocity.magnitude.ToString("n2");

    }

    private void Update()
    {
        if (startDebris != 0 && curDebris != 0)
        {
            progressSlider.value = startDebris / curDebris;
        }
    }
    void throttleContraints()
    {
        if (Input.GetKeyDown("left ctrl") || Input.GetKeyDown("left shift"))
        {
            if (curThrust > 0)
                throttlePos = 1;
            if (curThrust < 0)
                throttlePos = -1;
            if (curThrust == 0)
            {
                if (Input.GetAxis("Uppy") > 0)
                    throttlePos = 1;
                else
                    throttlePos = -1;
            }
        }

        if (throttlePos == 1)
        {
            curThrust += Input.GetAxis("Uppy") * Time.deltaTime * throttleForce;
            curThrust = Mathf.Clamp(curThrust, 0, maxThrust);

        }
        if (throttlePos == -1)
        {
            curThrust += Input.GetAxis("Uppy") * Time.deltaTime * throttleForce;
            curThrust = Mathf.Clamp(curThrust, -maxThrust, 0);
        }
        if (throttlePos == 0)
        {
            curThrust += Input.GetAxis("Uppy") * Time.deltaTime * throttleForce;

        }
    }

}
