using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponSOGen : ScriptableObject
{
    public string weaponName;
    public GameObject weaponPrefab;
    public float damage;
    public float range;
}
