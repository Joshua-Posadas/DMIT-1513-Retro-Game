using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float movementSpeed = 6f;
    public float gravity = -9.81f;

    [Header("Input Actions")]
    public InputAction movementInput;

    private CharacterController controller;
    private Vector2 moveVector;
    private Vector3 velocity;
    private bool isGrounded;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        movementInput.Enable();
        movementInput.performed += ctx => moveVector = ctx.ReadValue<Vector2>();
        movementInput.canceled += ctx => moveVector = Vector2.zero;
    }

    private void Update()
    {
        HandleMovement();
        HandleGravity();
    }

    private void HandleMovement()
    {
        Vector3 move = transform.right * moveVector.x + transform.forward * moveVector.y;
        controller.Move(move * movementSpeed * Time.deltaTime);
    }

    private void HandleGravity()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    public float GetCurrentSpeed()
    {
        return moveVector.magnitude;
    }
}
