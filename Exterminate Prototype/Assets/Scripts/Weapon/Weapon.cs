using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Equiping")]
    public WeaponSOGen[] loadout;
    public Transform weaponParent;

    private int currentWeapon = -1;

    private float damage;
    private float range = 100f;

    [SerializeField] private Camera fpsCam;

    void Start()
    {
        Equip(0);
        damage = loadout[currentWeapon].damage;
        range = loadout[currentWeapon].range;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Equip(0);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Equip(int loadoutIndex)
    {
        //Prevent creating same weapon
        if (currentWeapon == loadoutIndex)
        {
            return;
        }

        GameObject weapon = Instantiate(loadout[loadoutIndex].weaponPrefab, weaponParent.position, weaponParent.rotation, weaponParent);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localEulerAngles = Vector3.zero;

        currentWeapon = loadoutIndex;
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
