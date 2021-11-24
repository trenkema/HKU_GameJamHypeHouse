using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    public Animator anim;
    private GameObject cam;

    public GameObject enableText;
    public GameObject disableText;
    public GameObject lockedText;

    public string AnimationBool;
    public string AnimationTrigger;

    public bool locked = false;
    public GameObject key;
    private PickUp player;

    public LayerMask IgnoreMe;
    public float rayDistance = 1.5f;
    RaycastHit hit;
    Ray ray;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PickUp>();
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
                if (objectHit.gameObject == gameObject && anim.GetBool(AnimationBool) == false && locked == false)
                {
                    anim.SetBool(AnimationBool, true);
                    anim.SetTrigger(AnimationTrigger);
                }
                else if (objectHit.gameObject == gameObject && anim.GetBool(AnimationBool) == false && locked == true)
                {
                    if (player.holdingObject == key && player.isInspecting == false)
                    {
                        player.UnRegisterOutlineObject(player.holdingObject);
                        Destroy(player.holdingObject);
                        player.isHolding = false;
                        locked = false;
                        anim.SetBool(AnimationBool, true);
                        anim.SetTrigger(AnimationTrigger);
                    }
                }
                else if (objectHit.gameObject == gameObject && anim.GetBool(AnimationBool) == true)
                {
                    anim.SetBool(AnimationBool, false);
                    anim.SetTrigger(AnimationTrigger);
                }
            }
        }
    }

    private void MouseOver()
    {
        if (Physics.Raycast(ray, out hit, rayDistance, ~IgnoreMe))
        {
            Transform objectHit = hit.transform;

            if (objectHit.gameObject == gameObject && anim.GetBool(AnimationBool) == false && locked == false)
            {
                disableText.SetActive(false);
                enableText.SetActive(true);
            }
            else if (objectHit.gameObject == gameObject && anim.GetBool(AnimationBool) == false && locked == true)
            {
                lockedText.SetActive(true);
                disableText.SetActive(false);
                enableText.SetActive(false);
            }
            else if (objectHit.gameObject == gameObject && anim.GetBool(AnimationBool) == true)
            {
                enableText.SetActive(false);
                disableText.SetActive(true);
                lockedText.SetActive(false);
            }
        }
        else
        {
            enableText.SetActive(false);
            disableText.SetActive(false);
            lockedText.SetActive(false);
        }
    }
}
