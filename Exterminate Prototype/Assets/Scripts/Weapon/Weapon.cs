using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Equiping")]
    public PlayerWeapon[] loadout;
    public Transform weaponParent;

    private int currentWeaponIndex = -1;
    private PlayerWeapon currentWeapon;

    private float damage;
    private float range;

    private bool isReloading = false;
    private Animator animator;

    [SerializeField] private Camera fpsCam;

    void Start()
    {
        Equip(0);
        damage = currentWeapon.damage;
        range = currentWeapon.range;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !isReloading)
        {
            Equip(0);
        }

        if (isReloading)
        {
            return;
        }

        if (currentWeapon.ammoInClip <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

/*        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }*/
    }

    void Equip(int loadoutIndex)
    {
        //Prevent creating same weapon
        if (currentWeaponIndex == loadoutIndex)
        {
            return;
        }

        GameObject weapon = Instantiate(loadout[loadoutIndex].weaponPrefab, weaponParent.position, weaponParent.rotation, weaponParent);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localEulerAngles = Vector3.zero;

        isReloading = false;
        animator = weapon.GetComponent<Animator>();

        currentWeaponIndex = loadoutIndex;
        currentWeapon = loadout[loadoutIndex];
    }

    public void Shoot()
    {
        if (isReloading)
        {
            return;
        }
        currentWeapon.ammoInClip--;

        //Play muzzleflash particles

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }

            //Debug.Log(hit.transform.name);
        }

        Debug.Log("Pew");
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(currentWeapon.reloadTime - .25f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(.25f);

        currentWeapon.ammoInClip = currentWeapon.clipSize;
        isReloading = false;
    }
}
