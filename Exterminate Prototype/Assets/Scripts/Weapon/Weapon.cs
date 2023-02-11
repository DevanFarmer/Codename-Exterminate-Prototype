using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private InputManager inputManager;

    [Header("Equiping")]
    public PlayerWeapon[] loadout;
    public Transform weaponParent;

    private int currentWeaponIndex = -1;
    public PlayerWeapon currentWeapon;

    [SerializeField] private bool isReloading = false;
    [SerializeField] private bool readyToShoot = true;
    //[SerializeField] private float nextTimeToFire = 0f;

    private ParticleSystem muzzleFlash;

    private Animator animator;

    [SerializeField] private Camera fpsCam;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
    }

    void Start()
    {
        Equip(0);
    }

    void Update()
    {
        if (isReloading)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Equip(0);
        }

        if (currentWeapon.ammoInClip <= 0)
        {
            ReloadWeapon();
            return;
        }

        Shoot();
    }

    void Equip(int loadoutIndex)
    {
        //Prevent creating same weapon
        if ((currentWeaponIndex == loadoutIndex) || loadoutIndex >= loadout.Length)
        {
            return;
        }

        GameObject weapon = Instantiate(loadout[loadoutIndex].weaponPrefab, weaponParent.position, weaponParent.rotation, weaponParent);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localEulerAngles = Vector3.zero;

        isReloading = false;
        animator = weapon.GetComponent<Animator>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();

        currentWeaponIndex = loadoutIndex;
        currentWeapon = loadout[loadoutIndex];
    }

    public void Shoot()
    {
        if (isReloading) return;
        if (!readyToShoot) return;
        if (!inputManager.firing) return;

        readyToShoot = false;

        currentWeapon.ammoInClip--;

        //Play muzzleflash particles
        muzzleFlash.Play();

        //Fire Raycast
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, currentWeapon.range))
        {
            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(currentWeapon.damage);
            }
        }

        Invoke("ResetShot", 1f/currentWeapon.fireRate);

        //Reset variable to false to only allow shooting every press of the fire button
        if (!currentWeapon.auto)
        {
            inputManager.DeactivateFiring();
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    IEnumerator Reload()
    {
        readyToShoot = false;

        isReloading = true;
        Debug.Log("Reloading...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(currentWeapon.reloadTime - .25f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(.25f);

        currentWeapon.ammoInClip = currentWeapon.clipSize;
        isReloading = false;

        ResetShot();
    }

    public void ReloadWeapon()
    {
        if (isReloading)
        {
            return;
        }
        if (currentWeapon.ammoInClip == currentWeapon.clipSize)
        {
            return;
        }
        StartCoroutine(Reload());
    }
}
