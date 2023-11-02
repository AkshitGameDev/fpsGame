using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private void Awake()
    {
        instance = this;
    }

    [Header("Movement")]
    Transform playerTransform;
    [SerializeField]
    private float moveSpeed = 10f;
    public float groundDrag = 0.1f;
    Vector3 moveDirection = new Vector3();
    float horizontalInput;
    float verticalInput;
    public Rigidbody rb;

    [Header("groundCheck")]
    public float playerHight = 2;
    public LayerMask whatIsGround;
    bool grounded;


    [Header("Camera")]
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
        Debug.Log("camMain is", cameraNode);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        SlopeAngleCalculator.instance.AngleCalculator();
        CameraLookAround();

        myInput();
        speedControl();

        if (isOnGround()) { rb.drag = groundDrag; Debug.Log("Grounded :" + isOnGround()); }
        else rb.drag = 0;
    }

    

    private void FixedUpdate()
    {
        movePlayer();   
    }

    public bool isOnGround()
    {
        Debug.Log("groundCheck");
        return grounded = Physics.Raycast(transform.position, Vector3.down, playerHight * 2f + 0.05f, whatIsGround);
    }

    private void CameraLookAround()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * senX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * senY;

        yRot += mouseX;
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        cameraNode.transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        transform.rotation = Quaternion.Euler(0, yRot, 0);
    }
    void myInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    private void movePlayer()
    {
        Debug.Log("Masti");
        moveDirection = playerTransform.forward * verticalInput + playerTransform.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10, ForceMode.Force);
    }
    private void speedControl()
    {
        // if you go faster than your move speed this will normalize and set it to desired speed(comment from akshit)
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitVel.x, rb.velocity.y, limitVel.z);
        }
    }
}