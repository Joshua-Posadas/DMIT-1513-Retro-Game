using UnityEngine;

public class WeaponKickback : MonoBehaviour
{
    public float kickDistance = 0.05f;
    public float kickRecovery = 10f;

    private Vector3 initialLocalPos;
    private Vector3 currentOffset;
    private Vector3 velocity;

    public Vector3 CurrentKickOffset => currentOffset;

    private void Start()
    {
        initialLocalPos = transform.localPosition;
    }

    private void Update()
    {
        currentOffset = Vector3.SmoothDamp(currentOffset, Vector3.zero, ref velocity, 1f / kickRecovery);
    }

    public void ApplyKick()
    {
        currentOffset -= new Vector3(0, 0, kickDistance);
    }
}