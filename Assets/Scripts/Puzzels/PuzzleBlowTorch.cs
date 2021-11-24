using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBlowTorch : MonoBehaviour
{
    private GameObject cam;

    public GameObject enableText;
    public GameObject disableText;

    public GameObject[] lightSources;
    private List<bool> blownOff = new List<bool>();

    public AudioSource feedbackAudio;
    public AudioClip blowSound;

    public GameObject key;
    public GameObject lightKey;
    public bool isActive = false;

    public LayerMask IgnoreMe;
    public float rayDistance = 1.5f;
    RaycastHit hit;
    Ray ray;

    public GameObject[] endCandlesToEnable;

    void Start()
    {
        isActive = false;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        foreach (GameObject lights in lightSources)
        {
            blownOff.Add(false);
        }

        foreach (GameObject item in endCandlesToEnable)
        {
            item.SetActive(false);
        }
    }

    void Update()
    {
        ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        MouseOver();
        MouseDown();

        if (AllLightsOut() == true && isActive == false)
        {
            isActive = true;
            key.SetActive(true);
            lightKey.SetActive(true);

            foreach (GameObject item in endCandlesToEnable)
            {
                item.SetActive(true);
            }
        }
    }

    private void MouseDown()
    {
        if (Physics.Raycast(ray, out hit, rayDistance, ~IgnoreMe))
        {
            Transform objectHit = hit.transform;

            if (Input.GetMouseButtonDown(0))
            {
                for (int i = 0; i < lightSources.Length; i++)
                {
                    if (objectHit.gameObject == lightSources[i] && blownOff[i] == false)
                    {
                        lightSources[i].transform.GetChild(0).gameObject.SetActive(false);
                        blownOff[i] = true;
                        feedbackAudio.clip = blowSound;
                        feedbackAudio.Play();
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
            else if (objectHit.gameObject.tag == "LightsTrigger" && objectHit.gameObject.transform.GetChild(0).gameObject.activeSelf == true)
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

    private bool AllLightsOut()
    {
        for (int i = 0; i < blownOff.Count; i++)
        {
            if (blownOff[i] == false)
                return false;
        }

        return true;
    }
}
