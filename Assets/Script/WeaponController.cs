using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [Header("weapon Management")]
    [SerializeField] Weapon[] weapons;  //Array of all weapons
    [SerializeField] int currentWeaponIndex = 0; //currently equipped weapon

    private Weapon _currentWeapon; //refrence to current weapon

    //Weapon Images
    [SerializeField] RawImage shotGunImage;
    [SerializeField] RawImage pistolImage;
    //Player Images
    [SerializeField] GameObject pistolPlayer;
    [SerializeField] GameObject shotgunPlayer;


    private void Start()
    {
        //disabling all weapon first game start
        foreach(Weapon weapon in weapons)
        {
            if(weapon != null)
            {
                weapon.gameObject.SetActive(false);
            }
        }

        //initiliaze with first weapon
        SwitchWeapon(currentWeaponIndex);
    }

    private void Update()
    {
        //weapon switching with number keys
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0); //pistiol
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(1); //shortgun
        }

        //firing
        if(Input.GetMouseButtonDown(0))
        {
            FireCurrentWeapon();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadCurrentWeapon();
        }
    }

    private void ReloadCurrentWeapon()
    {
        _currentWeapon?.Reload();
    }

    private void FireCurrentWeapon()
    {
        _currentWeapon?.Fire();
    }

    public void SwitchWeapon(int newIndex)
    {
        //validate index;
        if (newIndex < 0  || newIndex >= weapons.Length) 
            return;

        //disable current weapon
        if(_currentWeapon != null)
        {
            _currentWeapon.gameObject.SetActive(false);

            DisableCurrentImage();

        }

        //enable new weapon
        currentWeaponIndex = newIndex;
        _currentWeapon = weapons[newIndex];
        _currentWeapon.gameObject.SetActive(true);
        _currentWeapon.ResetFireTimer();
        EnableCurrentImage();



    }

    private void EnableCurrentImage()
    {
        if(currentWeaponIndex == 0)
        {
            shotGunImage.gameObject.SetActive(true);
            shotgunPlayer.SetActive(false);
        }
        else if (currentWeaponIndex == 1)
        {
            pistolImage.gameObject.SetActive(true); 
            pistolPlayer.SetActive(false);
        }
    }

    private void DisableCurrentImage()
    {
        if (currentWeaponIndex == 0)
        {
            shotGunImage.gameObject.SetActive(false);
            shotgunPlayer.SetActive(true);
        }
        else if (currentWeaponIndex == 1)
        {
            pistolImage.gameObject.SetActive(false);
            pistolPlayer.SetActive(true);
        }
    }
}
