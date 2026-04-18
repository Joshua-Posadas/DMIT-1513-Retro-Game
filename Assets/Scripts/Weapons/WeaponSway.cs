using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSway : MonoBehaviour
{
    public float swayAmount = 0.05f;
    public float maxSway = 0.1f;
    public float smooth = 8f;

    private Vector3 initialLocalPos;

    private void Start()
    {
        initialLocalPos = transform.localPosition;
    }

    private void Update()
    {
        if (Mouse.current == null)
            return;

        float mouseX = Mouse.current.delta.x.ReadValue();
        float mouseY = Mouse.current.delta.y.ReadValue();

        mouseX *= 0.02f;
        mouseY *= 0.02f;

        Vector3 targetOffset = new Vector3(
            -mouseX * swayAmount,
            -mouseY * swayAmount,
            0
        );

        targetOffset.x = Mathf.Clamp(targetOffset.x, -maxSway, maxSway);
        targetOffset.y = Mathf.Clamp(targetOffset.y, -maxSway, maxSway);

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            initialLocalPos + targetOffset,
            Time.deltaTime * smooth
        );
    }
}