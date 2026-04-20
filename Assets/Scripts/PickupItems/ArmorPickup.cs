using UnityEngine;

public class ArmorPickup : MonoBehaviour
{
    public int armorAmount = 25;

    private void OnTriggerEnter(Collider other)
    {
        PlayerStats stats = other.GetComponentInParent<PlayerStats>();
        if (stats == null)
            return;

        if (stats.currentArmor >= stats.maxArmor)
            return;

        stats.currentArmor = Mathf.Clamp(stats.currentArmor + armorAmount, 0, stats.maxArmor);
        stats.UpdateUI();

        PickupAudioManager.Instance.PlayPickupSound();
        Destroy(gameObject);
    }
}
