using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public static bool cursorLocked = true;

    public GameObject cam;

    public float mouseSensitivityX = 100f, mouseSensitivityY = 100f;
    public float angleX, angleY;

    void Start()
    {
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        angleX += mouseX * Time.deltaTime * mouseSensitivityX;
        angleY += mouseY * Time.deltaTime * mouseSensitivityY;
        angleY = Mathf.Clamp(angleY, -89f, 89f);

        transform.rotation = Quaternion.Euler(0, angleX, 0);
        cam.transform.localRotation = Quaternion.Euler(-angleY, 0, 0);

        UpdateCursorLock();
    }

    void UpdateCursorLock()
    {
        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cursorLocked = false;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cursorLocked = true;
            }
        }
    }
}
