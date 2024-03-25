using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerControls playerControls;

    private Vector2 movementInput;
    [SerializeField] private float speed = 2f;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        OnMove();
    }

    private void PlayerInput()
    {
        movementInput = playerControls.Movement.Move.ReadValue<Vector2>();
    }

    private void OnMove()
    {
        rb.velocity = Vector3.zero;
        Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y);
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
