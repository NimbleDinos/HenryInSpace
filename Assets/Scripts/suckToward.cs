using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suckToward : MonoBehaviour
{
    public HenryController henryController;
    public GameObject target;
    public bool isDebris = true;
    public List<GameObject> curDebris;
    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("snootEnd");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!isDebris)
        {
            Debug.Log("Test");

            if(collision.gameObject.tag == "Debris")
            {
                henryController.curDebris--;
                Destroy(collision.gameObject);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(curDebris.Count != 0)
        {
            if (curDebris[0] == null)
            {
                curDebris.Remove(curDebris[0]);
            }
        }

        if (isDebris)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 1);

            transform.LookAt(2 * transform.position - target.transform.position);            
        }
        else if(curDebris.Count != 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, curDebris[0].transform.position, 1);

            transform.LookAt(2 * transform.position - curDebris[0].transform.position);

            Debug.Log(Vector3.Distance(curDebris[0].transform.position, transform.position));

            for (int i = 0; i < curDebris.Count; i++)
            {


                if (Vector3.Distance(curDebris[i].transform.position, transform.position) < 2)
                {
                    henryController.curDebris--;
                    Destroy(curDebris[i].gameObject);
                    curDebris.Remove(curDebris[i]);
                }
            }
        }
    }
}
