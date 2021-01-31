using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllSwitcher : MonoBehaviour
{

    public static int controlMode;

    public GameObject ThirdPersonCamera;
    public GameObject FirstPersonCamera;
    public GameObject ship;
    public GameObject player;

    public bool lookingAtChair = false;

    // Start is called before the first frame update
    void Start()
    {
        FirstPersonCamera.SetActive(true);
        ThirdPersonCamera.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (controlMode == 1 /*1 = Walking, 0 = Flying Ship */ && lookingAtChair == true)
            {
                controlMode = 0;
            }
            else
            {
                controlMode = 1;
            }
        }
    }

    void Caster(Vector2 center, float radius) 
    {
         Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius);
         int i = 0;
         while (i < hitColliders.Length) 
         {
             if(hitColliders[i].tag=="Chair")
                 {
                     lookingAtChair = true;
                 }
             i++;
        }
    }

    void ChangeController()
    {
        if (controlMode == 1)
        {
            ship.GetComponent<HenryController>().enabled = false;
            player.GetComponent<PlayerControler>().enabled = true;
            ThirdPersonCamera.SetActive(false);
            FirstPersonCamera.SetActive(true);
        }
        else if
        {
            ship.GetComponent<HenryController>().enabled = true;
            player.GetComponent<PlayerControler>().enabled = false;
            ThirdPersonCamera.SetActive(true);
            FirstPersonCamera.SetActive(false);
        }
    }
}
