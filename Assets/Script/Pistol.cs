using System;
using TMPro;
using UnityEngine;

public class Pistol : Weapon
{
    public TextMeshProUGUI pistolText;
    public TextMeshProUGUI pistolReloadStatus;



    private void Start()
    {
        _bulletOnMag = magazineCapacity;
        reloadTime = 2f;
    }

    private void Update()
    {
        PistolText();
    }

    private void PistolText()
    {
        pistolText.text = _bulletOnMag.ToString()+"/"+_totalBullet ;

        pistolReloadStatus.text = isReloading ? "Reloading...!" : "Ready!";
    }

    public override void Fire()  //must be overridden
    {
        if(!CanFire() || isReloading)
            return;

        //Single bullet in the direction the firePoint is facing
        CreateBullet(firePoint.up);  //function call from the weaponController class

        //updating the timer
        nextFireTime = Time.time + fireRate;
        Debug.Log("Pistol Fired");

        //reducing the bullets
        _bulletOnMag --;

        //autoreload when magazing is empty
        if (_bulletOnMag <= 0 && _totalBullet > 0)
        {
            Reload();
        }
    }

    public void GetPistolDrop()
    {
        _totalBullet += 10f;
    }

    
    }
    



    

