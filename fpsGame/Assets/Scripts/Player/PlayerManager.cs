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

    public MoveState state;
    public enum MoveState
    {
        wallkig,
        sprinting,
        crouching,
        air
    }

    [Header("Movement")]
    Transform playerTransform;
    [SerializeField]
    private float moveSpeed = 10f;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    public float groundDrag = 0.1f;
    Vector3 moveDirection = new Vector3();
    float horizontalInput;
    float verticalInput;
    public Rigidbody rb;
    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier;
    [SerializeField]
    bool readytoJummp = true;

    [Header("Crouching")]
    public float crouchspeed = 4.0f;
    public float crouchYscale;
    public float startYscale;

    [Header("groundCheck")]
    public float playerHight = 2;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope")]
    public float maxSlope = 30f;
    private RaycastHit sloppeHit;

    [Header("Camera")]
    public GameObject orientation = null;
    public Camera camMain = null;
    public GameObject cameraNode = null;

    public float senX = 0;
    public float senY = 0;
    
    float xRot = 0;
    float yRot = 0;

    [Header("KeyBindings")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode SprintKey = KeyCode.LeftShift;
    public KeyCode CrouchKey = KeyCode.LeftControl;


    // Start is called before the first frame update
    void Start()
    {
        startYscale = transform.localScale.y;
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
        stateHandler();

        if (isOnGround()) { rb.drag = groundDrag; Debug.Log("Grounded :" + isOnGround()); }
        else rb.drag = 0;

        Debug.Log("ground check " + isOnGround());
    }

    public void stateHandler()
    {
        if (Input.GetKey(CrouchKey))
        {
            state = MoveState.crouching;
            moveSpeed = crouchspeed;
        }
        else if (grounded && Input.GetKey(SprintKey))
        {
            state = MoveState.sprinting;
            moveSpeed = runSpeed;
        }
        else if(grounded)
        {
            state = MoveState.wallkig;
            moveSpeed = walkSpeed;
        }
        else
        {
            state = MoveState.air;
        }
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

        if (isOnGround() && readytoJummp && Input.GetKey(jumpKey))
        {
            readytoJummp = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCoolDown);
        }

        if(Input.GetKeyDown(CrouchKey)) {
            transform.localScale  = new Vector3(transform.localScale.x, crouchYscale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        else if(Input.GetKeyUp(CrouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYscale, transform.localScale.z);
        }
    }
    private void movePlayer()
    {
        Debug.Log("Masti");
        moveDirection = playerTransform.forward * verticalInput + playerTransform.right * horizontalInput;
        if(isOnGround()) rb.AddForce(moveDirection.normalized * moveSpeed * 10 * rb.mass, ForceMode.Force);
        else rb.AddForce(moveDirection.normalized * moveSpeed * 10 * rb.mass * airMultiplier, ForceMode.Force);
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
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f , rb.velocity.z);
        rb.AddForce(transform.up * jumpForce * (rb.mass/2), ForceMode.Impulse);
    }
    public void ResetJump()
    {
         readytoJummp = true;
    }
    private bool onSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out sloppeHit, playerHight * 0.5f + 0.3f))
        {

        }
        return readytoJummp;
    }
}