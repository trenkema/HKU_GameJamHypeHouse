using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityModifierRB : MonoBehaviour
{
    public Vector3 gravity = new Vector3(0f, -9.81f, 0f);

    private void Awake()
    {
        Physics.gravity = gravity;
    }
    //var gravity = Vector3 (0, -9.81, 0);

    // awa () 
    //{
    //     Physics.gravity = gravity;
    //}
}
