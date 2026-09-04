using System;
using UnityEngine;

[Serializable]
public class WeaponAmmo
{
    public string weaponName;
    public Transform bulletSpawn;
    public float fireRate;
    public float gunShotRadius;
    public float bulletDamage;
    public float bulletSpeed;
    public int currentMag;
    public int maxMag;
    public int reserveAmmo;
}

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] int selectedWeapon = 0;
    [SerializeField] AmmoUI ammoUI;
    [SerializeField] WeaponAmmo[] weapons;

    public WeaponAmmo CurrentWeapon => weapons[selectedWeapon];

    private void Start()
    {
        if (ammoUI == null)
        {
            ammoUI = FindAnyObjectByType<AmmoUI>();
        }

        SetWeapon(selectedWeapon);
    }

    public void SelectWeapon()
    {
        selectedWeapon = (selectedWeapon + 1) % transform.childCount;
        SetWeapon(selectedWeapon);
    }

    private void SetWeapon(int weaponIndex)
    {
        selectedWeapon = weaponIndex;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == selectedWeapon);
        }

        WeaponAmmo weapon = CurrentWeapon;

        ammoUI?.UpdateGunName(weapon.weaponName);
        ammoUI?.UpdateAmmoUI(weapon.currentMag, weapon.maxMag);
    }
}
