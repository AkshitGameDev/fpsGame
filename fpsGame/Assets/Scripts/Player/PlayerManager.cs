using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    //movement
    public float Speed = 10f;
    

    public GameObject orientation = null;
    public Camera camMain = null;
    public GameObject cameraNode = null;

    public float senX = 0;
    public float senY = 0;
    
    float xRot = 0;
    float yRot = 0;
    // Start is called before the first frame update
    void Start()
    {
        camMain = Camera.main;
        Console.WriteLine("camMain is", cameraNode);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * senX;
        float mouseY = Input.GetAxisRaw("Mouse y") * Time.deltaTime * senY;

        yRot += mouseX;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        cameraNode.transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        orientation.transform.rotation = Quaternion.Euler(0,yRot, 0);

    }
}
