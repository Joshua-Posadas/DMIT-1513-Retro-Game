using UnityEngine;

public class MuzzleFlashLight : MonoBehaviour
{
    public Light flashLight;
    public float flashDuration = 0.05f;

    private float timer;

    private void Start()
    {
        if (flashLight != null)
            flashLight.enabled = false;
    }

    private void Update()
    {
        if (flashLight != null && flashLight.enabled)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
                flashLight.enabled = false;
        }
    }

    public void TriggerFlash()
    {
        if (flashLight == null)
            return;

        flashLight.enabled = true;
        timer = flashDuration;
    }
}
