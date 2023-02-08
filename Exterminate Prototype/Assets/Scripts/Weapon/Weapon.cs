using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponSOGen[] loadout;
    public Transform weaponParent;

    private float currentWeapon = -1;

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Equip(0);
        }
    }

    void Equip(int loadoutIndex)
    {
        //Prevent creating same weapon
        if (currentWeapon == loadoutIndex)
        {
            return;
        }

        GameObject weapon = Instantiate(loadout[loadoutIndex].weaponPrefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localEulerAngles = Vector3.zero;

        currentWeapon = loadoutIndex;
    }
}
