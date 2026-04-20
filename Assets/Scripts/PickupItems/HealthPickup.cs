using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 25;

    private void OnTriggerEnter(Collider other)
    {
        PlayerStats stats = other.GetComponentInParent<PlayerStats>();
        if (stats == null)
            return;

        if (stats.currentHealth >= stats.maxHealth)
            return;

        stats.currentHealth = Mathf.Clamp(stats.currentHealth + healAmount, 0, stats.maxHealth);
        stats.UpdateUI();

        PickupAudioManager.Instance.PlayPickupSound();
        Destroy(gameObject);
    }
}
