using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ActiveWeapon))]
[RequireComponent(typeof(FireWeaponEvent))]
[RequireComponent(typeof(WeaponFiredEvent))]
[DisallowMultipleComponent]
public class FireWeapon : MonoBehaviour
{
    private float fireRateCoolDownTimer = 0f;
    private ActiveWeapon activeWeapon;
    private FireWeaponEvent fireWeaponEvent;
    private WeaponFiredEvent weaponFiredEvent;

    private void Awake()
    {
        activeWeapon = GetComponent<ActiveWeapon>();
        fireWeaponEvent = GetComponent<FireWeaponEvent>();
        weaponFiredEvent = GetComponent<WeaponFiredEvent>();
    }

    private void OnEnable()
    {
        fireWeaponEvent.OnFireWeapon += FireWeaponEvent_OnFireWeapon;
    }

    private void OnDisable()
    {
        fireWeaponEvent.OnFireWeapon -= FireWeaponEvent_OnFireWeapon;
    }

    private void Update()
    {
        fireRateCoolDownTimer -= Time.deltaTime;
    }

    private void FireWeaponEvent_OnFireWeapon(FireWeaponEvent fireWeaponEvent, FireWeaponEventArgs fireWeaponEventArgs)
    {
        WeaponFire(fireWeaponEventArgs);
    }

    private void WeaponFire(FireWeaponEventArgs fireWeaponEventArgs)
    {
        if (fireWeaponEventArgs.fire)
        {
            if (IsWeaponReadyToFire())
            {
                FireAmmo(fireWeaponEventArgs.aimAngle, fireWeaponEventArgs.weaponAimAngle,
                    fireWeaponEventArgs.weaponAimDirectionVector);

                ResetCoolDownTimer();
            }
        }
    }

    private bool IsWeaponReadyToFire()
    {
        if (activeWeapon.GetCurrentWeapon().weaponRemainingAmmo <= 0 && !activeWeapon.GetCurrentWeapon().weaponDetails.hasInfiniteAmmo)
        {
            return false;
        }

        if (activeWeapon.GetCurrentWeapon().isWeaponReloading)
        {
            return false;
        }

        if (fireRateCoolDownTimer > 0f)
        {
            return false;
        }

        if (!activeWeapon.GetCurrentWeapon().weaponDetails.hasInfiniteClipCapacity && activeWeapon.GetCurrentWeapon().weaponClipRemainingAmmo <= 0)
        {
            return false;
        }

        return true;
    }

    private void FireAmmo(float aimAngle, float weaponAimAngle, Vector3 weaponAimDirectionVector)
    {
        AmmoDetailsSO currentAmmo = activeWeapon.GetCurrentAmmo();

        if (currentAmmo != null)
        {
            //Get ammo prefav
            GameObject ammoPrefab = currentAmmo.ammoPrefabArray[Random.Range(0, currentAmmo.ammoPrefabArray.Length)];
            
            //Get Random speed
            float ammoSpeed = Random.Range(currentAmmo.ammoSpeedMin, currentAmmo.ammoSpeedMax);
            
            //Get gameobject with IFireable Component
            IFireable ammo = (IFireable)PoolManager.Instance.ReuseComponent(ammoPrefab, activeWeapon.GetShootPosition(),
                Quaternion.identity);
            
            //Initialise the ammo
            ammo.InitialiseAmmo(currentAmmo, aimAngle, weaponAimAngle, ammoSpeed, weaponAimDirectionVector);

            //Reduce clip ammo amount and the total ammo amount if it does not have infinite clip capacity
            if (!activeWeapon.GetCurrentWeapon().weaponDetails.hasInfiniteClipCapacity)
            {
                activeWeapon.GetCurrentWeapon().weaponClipRemainingAmmo--;
                activeWeapon.GetCurrentWeapon().weaponRemainingAmmo--;
            }
            
            weaponFiredEvent.CallOnWeaponFiredEvent(activeWeapon.GetCurrentWeapon());
        }
    }

    private void ResetCoolDownTimer()
    {
        fireRateCoolDownTimer = activeWeapon.GetCurrentWeapon().weaponDetails.weaponFireRate;
    }
}