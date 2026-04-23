using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    public InputAction rotationInput;
    public float sensitivity = 0.1f;
    public float maxPitch = 85f;

    private float yaw;
    private float pitch;

    private void Start()
    {
        rotationInput.Enable();
        rotationInput.performed += OnLook;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        if (PauseController.IsPaused || PlayerStats.IsDead)
            return;

        Vector2 mouseDelta = context.ReadValue<Vector2>();

        yaw += mouseDelta.x * sensitivity;
        pitch -= mouseDelta.y * sensitivity;
    }

    private void LateUpdate()
    {
        if (PauseController.IsPaused || PlayerStats.IsDead)
            return;

        pitch = Mathf.Clamp(pitch, -maxPitch, maxPitch);
        transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}
