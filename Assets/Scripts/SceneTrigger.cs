using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    private GameObject cam;

    public GameObject triggerText;

    public SceneTransitions NextSceneTransition;
    public bool objectTrigger = false;

    public LayerMask IgnoreMe;
    public float rayDistance = 1.5f;
    RaycastHit hit;
    Ray ray;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameObject.FindGameObjectWithTag("Player") && objectTrigger == false)
        {
            NextSceneTransition.endScene = true;
        }
    }

    void Update()
    {
        if (objectTrigger == true)
        {
            ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            MouseDown();
        }
    }

    private void MouseDown()
    {
        if (Physics.Raycast(ray, out hit, rayDistance, ~IgnoreMe))
        {
            Transform objectHit = hit.transform;

            if (objectHit.gameObject == gameObject)
            {
                triggerText.SetActive(true);
            }
            else
            {
                triggerText.SetActive(false);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (objectHit.gameObject == gameObject)
                {
                    NextSceneTransition.endScene = true;
                }
            }
        }
        else
        {
            triggerText.SetActive(false);
        }
    }
}
