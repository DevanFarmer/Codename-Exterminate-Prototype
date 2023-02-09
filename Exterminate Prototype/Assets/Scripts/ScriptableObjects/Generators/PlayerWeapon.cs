using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class PlayerWeapon : ScriptableObject
{
    public string weaponName;
    public GameObject weaponPrefab;
    public float damage;
    public float range;

    public float FireRate;
    public bool auto;

    public int maxAmmo;
    public int clipSize;
    public int ammoInClip;
    public float reloadTime;
}
