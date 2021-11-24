using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleObject : MonoBehaviour
{
    public GameObject[] objectsToScale;

    private void Start()
    {
        for (int i = 0; i < objectsToScale.Length; i++)
        {
            objectsToScale[i].transform.localScale = objectsToScale[i].transform.localScale * 100;
        }
    }
}
