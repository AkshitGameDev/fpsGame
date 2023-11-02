using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class SlopeAngleCalculator : MonoBehaviour
{
    RaycastHit hit;
    public GameObject test;

    public static SlopeAngleCalculator instance;
    private void Awake()
    {
        instance = this;
    }

    public void AngleCalculator()
    {
        Physics.Raycast(transform.position, Vector3.down, out hit);
        Debug.Log(hit.point);
        if (hit.point != null){ Vector3 targetDir = hit.point - transform.position;
        float anglex = Vector3.Angle(targetDir, Vector3.forward);
        Vector3 rot = new Vector3(anglex, 1, 0);
        test.transform.Rotate(rot);
        Debug.Log("slope angle" + rot); 
        }


    }

}
