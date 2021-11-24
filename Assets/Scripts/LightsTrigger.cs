using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsTrigger : MonoBehaviour
{
    private GameObject cam;

    public GameObject enableText;
    public GameObject disableText;

    public LayerMask IgnoreMe;
    public float rayDistance = 1.5f;
    RaycastHit hit;
    Ray ray;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        MouseOver();
        MouseDown();
    }

    private void MouseDown()
    {
        if (Physics.Raycast(ray, out hit, rayDistance, ~IgnoreMe))
        {
            Transform objectHit = hit.transform;

            if (Input.GetMouseButtonDown(0))
            {
                if (objectHit.gameObject.tag == "LightsTrigger")
                {
                    if (objectHit.gameObject.transform.GetChild(0).gameObject.activeSelf == true)
                    {
                        Debug.Log("Tried to set inactive");
                        objectHit.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    else if (objectHit.gameObject.transform.GetChild(0).gameObject.activeSelf == false)
                    {
                        objectHit.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                        Debug.Log("Tried to set active");
                    }
                }
            }
        }
    }

    private void MouseOver()
    {
        if (Physics.Raycast(ray, out hit, rayDistance, ~IgnoreMe))
        {
            Transform objectHit = hit.transform;

            if (objectHit.gameObject.tag == "LightsTrigger" && objectHit.gameObject.transform.GetChild(0).gameObject.activeSelf == false)
            {
                disableText.SetActive(false);
                enableText.SetActive(true);
            }
            if (objectHit.gameObject.tag == "LightsTrigger" && objectHit.gameObject.transform.GetChild(0).gameObject.activeSelf == true)
            {
                enableText.SetActive(false);
                disableText.SetActive(true);
            }
        }
        else
        {
            enableText.SetActive(false);
            disableText.SetActive(false);
        }
    }
}
