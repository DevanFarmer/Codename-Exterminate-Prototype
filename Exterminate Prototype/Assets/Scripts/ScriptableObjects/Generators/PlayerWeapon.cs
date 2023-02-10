using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class PlayerWeapon : ScriptableObject
{
    [Header("Base")]
    public string weaponName;
    public GameObject weaponPrefab;
    public float damage;
    public float range;

    [Header("Fire Mode")]
    public float fireRate;
    public bool auto;


    [Header("Ammo")]
    public int maxAmmo;
    public int clipSize;
    public int ammoInClip;
    public float reloadTime;
}
