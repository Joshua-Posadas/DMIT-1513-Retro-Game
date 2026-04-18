using UnityEngine;

public class Recoil : MonoBehaviour
{
    public float recoilKick = 2f;
    public float recoilRecovery = 8f;

    private float currentRecoil;
    private float recoilVelocity;

    private void Update()
    {
        currentRecoil = Mathf.SmoothDamp(currentRecoil, 0f, ref recoilVelocity, 1f / recoilRecovery);
        transform.localRotation = Quaternion.Euler(-currentRecoil, 0f, 0f);
    }

    public void ApplyRecoil()
    {
        currentRecoil += recoilKick;
    }
}