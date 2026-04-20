using UnityEngine;

public class ItemDirectionalSprite : MonoBehaviour
{
    private Transform cam;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (cam == null)
            return;

        Vector3 lookDir = transform.position - cam.position;
        lookDir.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDir);
    }
}
