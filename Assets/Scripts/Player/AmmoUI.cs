using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gunName;
    [SerializeField] Image ammo;

    public void UpdateGunName(string name)
    {
        if (gunName != null)
        {
            gunName.text = name;
        }
    }

    public void UpdateAmmoUI(int current, int maxAmmo)
    {
        if (ammo == null) return;

        ammo.fillAmount = Mathf.Clamp01((float)current / Mathf.Max(1, maxAmmo));
    }

    public void ShowReloading()
    {
        if (ammo == null) return;

        ammo.fillAmount = 0f;
    }
}
