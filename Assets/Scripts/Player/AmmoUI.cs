using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoText;

    public void UpdateAmmoUI(int current, int reserve)
    {
        ammoText.text = $"{current} / {reserve}";
    }
}
