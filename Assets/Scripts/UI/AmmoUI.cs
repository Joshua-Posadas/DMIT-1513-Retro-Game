using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    public RaycastGun gun;
    public TextMeshProUGUI ammoText;

    private void Update()
    {
        ammoText.text = gun.currentAmmo + "/" + gun.magazineSize;
    }
}
