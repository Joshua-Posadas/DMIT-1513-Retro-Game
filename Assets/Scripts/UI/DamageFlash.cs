using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageFlash : MonoBehaviour
{
    private Image overlay;
    public float flashDuration = 0.1f;
    public float maxAlpha = 0.1f;

    private Coroutine flashRoutine;

    private void Awake()
    {
        overlay = GetComponent<Image>();
    }

    public void TriggerFlash()
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        float t = 0f;
        while (t < flashDuration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(0, maxAlpha, t / flashDuration);
            SetAlpha(a);
            yield return null;
        }

        t = 0f;
        while (t < flashDuration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(maxAlpha, 0, t / flashDuration);
            SetAlpha(a);
            yield return null;
        }

        SetAlpha(0);
    }

    private void SetAlpha(float a)
    {
        Color c = overlay.color;
        c.a = a;
        overlay.color = c;
    }
}