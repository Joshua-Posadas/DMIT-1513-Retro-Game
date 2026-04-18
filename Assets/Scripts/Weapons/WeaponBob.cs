using UnityEngine;

public class WeaponBob : MonoBehaviour
{
    public float bobSpeed = 6f;
    public float bobAmount = 0.05f;
    public WeaponKickback kickback;

    private Vector3 initialLocalPos;
    private float bobTimer;

    private void Start()
    {
        initialLocalPos = transform.localPosition;
    }

    public void UpdateBob(float moveMagnitude)
    {
        float bobX = 0f;
        float bobY = 0f;

        if (moveMagnitude > 0.1f)
        {
            bobTimer += Time.deltaTime * bobSpeed;

            bobX = Mathf.Sin(bobTimer) * bobAmount;
            bobY = Mathf.Cos(bobTimer * 2f) * bobAmount * 0.5f;
        }
        else
        {
            bobTimer = 0f;
        }

        Vector3 kickOffset = kickback != null ? kickback.CurrentKickOffset : Vector3.zero;

        transform.localPosition = initialLocalPos + kickOffset + new Vector3(bobX, bobY, 0f);
    }
}