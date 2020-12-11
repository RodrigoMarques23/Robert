using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{

    public GameObject otherPlayer;
    public Camera cam;
    public Camera cam1;


    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
        cam1 = otherPlayer.GetComponentInChildren<Camera>();
    }
    void Update()
    {
        Switch();
    }

    void Switch()
    {
        if (Input.GetButtonDown("R"))
        {



            GetComponent<PlayerMovement>().enabled = false;
            cam.enabled = false;

            otherPlayer.GetComponent<PlayerMovement>().enabled = true;
            cam1.enabled = true;

        }

        if (Input.GetButton("E"))
        {
            otherPlayer.GetComponent<PlayerMovement>().enabled = false;
            cam1.enabled = false;

            GetComponent<PlayerMovement>().enabled = true;
            cam.enabled = true;

        }

    }
}