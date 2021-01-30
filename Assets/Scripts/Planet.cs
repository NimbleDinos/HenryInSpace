using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float G;

    Vector3 GetDistanceVector(Vector3 shipPosition)
    {
        return shipPosition - transform.position;
    }

    Vector3 CalculateGravity(GameObject ship)
    {
        Vector3 distanceVect = GetDistanceVector(ship.transform.position);
        Vector3 gravityDirection = distanceVect.normalized;
        float distance = distanceVect.magnitude;
        
        float gravity = G / (distance * distance);
        
        return gravityDirection * gravity;
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerStay(Collider ship)
    {
        Rigidbody shipBody = ship.GetComponent<Rigidbody>();
        if (shipBody)
        {
            Vector3 gravity = CalculateGravity(ship.gameObject);
            shipBody.AddForce(gravity, ForceMode.Acceleration);
        }
    }
}
