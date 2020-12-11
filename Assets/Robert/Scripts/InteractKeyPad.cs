using UnityEngine;
using UnityEngine.UI;

public class InteractKeyPad : MonoBehaviour
{
    public Transform headPos;

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(headPos.position, headPos.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            float distance = Vector3.Distance(transform.position, hit.transform.position);
            if (distance <= 3f)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.transform.GetComponent<KeyPress>() != null)
                    {
                        hit.transform.GetComponent <KeyPress>().SendKey();
                    }
                    else if (hit.transform.name == "Painting")
                    {
                        hit.transform.GetComponent<Painting>().OpenClose();
                    }
                }
            }
        }
    }
}
