using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public abstract class Weapon : MonoBehaviour
{

    //common properties of all Weapons
    [Header("Weapon Settings")]
    public float fireRate = 0.5f;
    public float damage = 10f;
    public float fireForce = 20f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    
    //Timer to control firing rate
    public float nextFireTime = 0f;

    public float magazineCapacity;
    protected float _bulletOnMag;
    protected float _totalBullet = 25f;

    protected bool isReloading = false;
    protected float reloadTime = 1f; // to  be overridden by child classes

    





    //Methods 

    public void ResetFireTimer()
    {
        nextFireTime = 0f;

    }

    // Virtual Method , can be overridden by child classes
    public virtual bool CanFire()
    {
        return (Time.time >= nextFireTime && _bulletOnMag != 0 && !isReloading);
        //checks if enough time has passed since last shot or it magazing have enough bullets
    }

    //Abstract Method , must be implementd by child classes
    public abstract void Fire();

    protected virtual bool IsAutomatic => false; // Default to single fire


    //common methods for all Weapons
    protected void CreateBullet(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // instantiation of the bullet
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>(); //refrence of rigidbody or bullet
        bulletRB.AddForce(direction *  fireForce, ForceMode2D.Impulse); //shooting of the bullet , the forece added
    }

    public void Reload()
    {
        //no reload
        if (isReloading || _bulletOnMag == magazineCapacity || _totalBullet <= 0)
        {
            return;
        } 

        //start relod coroutine
        StartCoroutine(ReloadCoroutine());

    }

    protected virtual IEnumerator ReloadCoroutine()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime); //different for each weapon

        //calculate how many needed to fill maz
        float needed = magazineCapacity - _bulletOnMag;

        if (_totalBullet >= needed)
        {
            //full reload
            _totalBullet -= needed;
            _bulletOnMag = magazineCapacity;
        }
        else
        {
            //partial reload with remaing bullets
            _bulletOnMag += _totalBullet;
            _totalBullet = 0;
        }

        isReloading = false;

    }

    public bool IsReloading() //to check if weapon is currently reloading;
    {
        return isReloading;
    }

    private void OnEnable()
    {
        // Reset reload state when weapon is enabled
        if (isReloading)
        {
            isReloading = false;
            Debug.LogWarning("Reload was interrupted, resetting reload state");
        }
    }



}
