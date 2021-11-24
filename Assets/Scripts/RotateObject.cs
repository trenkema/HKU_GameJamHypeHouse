using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 75f;

    public void OnMouseDrag()
    {
        float xRotation = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float yRotation = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // select the axis by which you want to rotate the GameObject
        transform.Rotate(Vector3.down, xRotation);
        transform.Rotate(Vector3.right, yRotation);
    }
}
