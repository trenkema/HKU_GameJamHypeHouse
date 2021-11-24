using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadBob : MonoBehaviour
{
    public float walkingBobbingSpeed = 14f;
    public float sprintingBobbingSpeed = 16f;
    public float bobbingAmount = 0.05f;
    public PlayerMovement controller;
    private bool isGrounded = true;

    float defaultPosY = 0;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        defaultPosY = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        if (Mathf.Abs(controller.x) > 0.1f || Mathf.Abs(controller.z) > 0.1f && isGrounded)
        {
            timer += Time.deltaTime * walkingBobbingSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
        }
        //else if (Mathf.Abs(controller.x) > 0.1f || Mathf.Abs(controller.z) > 0.1f && isGrounded && controller.isSprinting)
        //{
        //    timer += Time.deltaTime * sprintingBobbingSpeed;
        //    transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
        //}
        else
        {
            //Idle
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * walkingBobbingSpeed), transform.localPosition.z);
        }
    }
}
