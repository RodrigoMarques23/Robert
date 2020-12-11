using UnityEngine;
using System.Collections;

public class TeleportElevator : MonoBehaviour
{
    public GameObject ui;
    public GameObject objToTP;
    public Transform tpLoc;
    void Start()
    {
        ui.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {
        ui.SetActive(true);
        if ((other.gameObject.tag == "Itens") && Input.GetMouseButtonDown(0))
        {
            objToTP.transform.position = tpLoc.transform.position;
        }
    }
    void OnTriggerExit()
    {
        ui.SetActive(false);
    }
}
