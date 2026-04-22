using UnityEngine;

public class MuzzleFlashIMG : MonoBehaviour
{
    public float flashDuration = 0.05f;

    private MeshRenderer meshRenderer;
    private float timer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer != null)
            meshRenderer.enabled = false;
    }

    private void Update()
    {
        if (meshRenderer != null && meshRenderer.enabled)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
                meshRenderer.enabled = false;
        }
    }

    public void TriggerFlash()
    {
        if (meshRenderer == null)
            return;

        meshRenderer.enabled = true;
        timer = flashDuration;
    }
}