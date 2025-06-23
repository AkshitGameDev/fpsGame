using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    private CapsuleCollider collider;
    
    public float slowWalkSpeed = 1.0f;
    public float fastWalkSpeed = 1.8f;
    public float sprintSpeed = 2.4f;
    public float jumpHight = 2.0f;

    private bool isSprinting = false;
    private bool isJumping = false;
    private bool isCrouching = false;
    private bool grounded = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
