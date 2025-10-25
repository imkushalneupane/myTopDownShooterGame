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
    [SerializeField] RawImage AK47Image;

    //Player Images
    [SerializeField] GameObject handgunPlayer;
    [SerializeField] GameObject longgunPlayer;

    private bool IsAutomatic = false;


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
            IsAutomatic = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(1); //shortgun
            IsAutomatic= false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapon(2); //AK47
            IsAutomatic = true;
        }

        //firing
        if (_currentWeapon != null)
        {
            // Auto fire for AK47
            if (IsAutomatic && Input.GetMouseButton(0))
            {
                FireCurrentWeapon();
            }
            // Single fire for Pistol, Shotgun
            else if (!IsAutomatic && Input.GetMouseButtonDown(0))
            {
                FireCurrentWeapon();
            }
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

            DisableAllImage();

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
        switch (currentWeaponIndex)
        {
            case 0:
                pistolImage.gameObject.SetActive(true);
                handgunPlayer.SetActive(true);
                break;
            case 1:
                shotGunImage.gameObject.SetActive(true);
                longgunPlayer.SetActive(true);
                break;
            case 2:
                AK47Image.gameObject.SetActive(true);
                longgunPlayer.SetActive(true);
                break;

        }

    }

    private void DisableAllImage()
    {
        pistolImage.gameObject.SetActive(false);
        handgunPlayer.SetActive(false);
        shotGunImage.gameObject.SetActive(false);
        AK47Image.gameObject.SetActive(false);
        longgunPlayer.SetActive(false);

    }
}
