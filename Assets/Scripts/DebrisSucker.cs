using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisSucker : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().isTrigger = true;
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if(other.tag == "Debris")
    //    {
    //        Debug.Log("Boop");
    //        Vector3 direction = transform.position - other.transform.position;
    //        other.GetComponent<Rigidbody>().AddRelativeForce(direction.normalized * 30, ForceMode.Force);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Debris" || other.tag == "Fish")
        {
            //Debug.Log("Boop");
            //Vector3 direction = transform.position - other.transform.position;
            //other.GetComponent<Rigidbody>().AddRelativeForce(direction.normalized * 300, ForceMode.Force);
            other.GetComponent<suckToward>().enabled = true;
            other.GetComponent<OrbitFunction>().enabled = false;

            GetComponentInParent<suckToward>().curDebris.Add(other.gameObject);
        }
    }

    // Update is called once per frame

    void Update()
    {
        
    }
}
