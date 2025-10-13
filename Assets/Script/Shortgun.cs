using System;
using TMPro;
using UnityEngine;

public class Shortgun: Weapon
{
    [Header("ShortGun Settings")]
    public int PelletCount = 5;  //Number of Bullet per shot

    public TextMeshProUGUI shotGunText;
    public TextMeshProUGUI shotgunReloadStatus;


    private void Start()
    {
        _totalBullet = 12f;
        _bulletOnMag = magazineCapacity;
        reloadTime = 3f;
    }

    private void Update()
    {
        ShotGunText();
    }

    private void ShotGunText()
    {
        shotGunText.text = _bulletOnMag.ToString()+"/"+_totalBullet.ToString();
        shotgunReloadStatus.text = isReloading ? "Reloading...!" : "Ready!";
    }

    public override void Fire() //must override the abstract methods
    {
        Debug.Log("Shotrgun Fire!!");

        if (!CanFire())
        {
            Debug.Log("cannot fire!! shortgun");
            return;
            
        }
            
        CreateBullet(firePoint.up);

        //slower firerate of shortgun
        nextFireTime = Time.time + fireRate;

        //reducing the bullets
        _bulletOnMag--;

        //autoreload when magazing is empty
        if (_bulletOnMag <= 0 && _totalBullet > 0)
        {
            Reload();
        }
    }

    //need to override CanFire to add custom logic if needed
    public override bool CanFire()
    {
        bool canFire = base.CanFire();
        Debug.Log($"shotgun canfire: {canFire} (Time: {Time.time}, NextFire: {nextFireTime})");

        return canFire;

    }

    public void GetShotgunDrop()
    {
        _totalBullet += 6f;
    }

}
