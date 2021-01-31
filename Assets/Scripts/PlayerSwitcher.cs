using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitcher : MonoBehaviour
{
    public Camera playerCam, henryCam;
    public HenryController henryController;
    public PlayerControler playerControler;

    bool isHenry = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("c"))
        {
            if (isHenry)
            {
                playerCam.gameObject.SetActive(true);
                playerControler.enabled = true;
                henryController.enabled = false;
                henryCam.gameObject.SetActive(false);

                isHenry = !isHenry;
            }
            else
            {
                playerCam.gameObject.SetActive(false);
                playerControler.enabled = false;
                henryController.enabled = true;
                henryCam.gameObject.SetActive(true);
                isHenry = !isHenry;
            }
        }


    }
}
