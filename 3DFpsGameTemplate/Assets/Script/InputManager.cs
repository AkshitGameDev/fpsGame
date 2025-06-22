using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.PlayerMovementActions movementActions;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        movementActions = playerInput.PlayerMovement;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        movementActions.Enable();
    }
    private void OnDisable()
    {
        movementActions.Disable();
    }
}
