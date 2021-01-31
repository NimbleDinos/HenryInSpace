using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suckToward : MonoBehaviour
{
    public GameObject target;
    public bool isDebris = true;
    public List<GameObject> curDebris;
    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("snootEnd");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDebris)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 1);

            transform.LookAt(2 * transform.position - target.transform.position);
        }
        else if(curDebris.Count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, curDebris[0].transform.position, 1);

            transform.LookAt(2 * transform.position - curDebris[0].transform.position);
        }
    }
}
