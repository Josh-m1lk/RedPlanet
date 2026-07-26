using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoText;

    public void UpdateAmmoUI(int current, int reserve)
    {
        ammoText.text = $"{current} / {reserve}";//Will show ammo in current mag and reserve pile
    }

    public void ShowReloading(int reserve)
    {
        ammoText.text = $"Reloading / {reserve}";//Will show reloading instead of current ammo but will still show reserve
    }
}
