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
    [Tooltip("Higher value equals faster shooting, don't go too high: Max 10")]
    [Range(1, 10)]
    public float fireRate;
    public bool auto;


    [Header("Ammo")]
    [Tooltip("The maximum out of ammo the player can carry for this gun.")]
    public int maxAmmo;
    [Tooltip("Size of gun clip.")]
    public int clipSize;
    [Tooltip("Current amount of ammo in gun clip")]
    public int ammoInClip;
    [Tooltip("The time it takes, in seconds, to reload")]
    [Range(1, 2)]
    public float reloadTime;
}
