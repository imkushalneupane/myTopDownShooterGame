using System;
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
    public static bool HasAK47 = false;

    // Mobile Input
    private PlayerInput playerInput;
    private InputAction fireAction;
    private InputAction reloadAction;
    private InputAction switchWeaponAction;

    private void Start()
    {
        //disabling all weapon first game start
        foreach (Weapon weapon in weapons)
        {
            if (weapon != null)
            {
                weapon.gameObject.SetActive(false);
            }
        }

        //initiliaze with first weapon
        SwitchWeapon(currentWeaponIndex);

        // Get mobile input actions
        playerInput = GetComponent<PlayerInput>();
        fireAction = playerInput.actions["Fire"];
        reloadAction = playerInput.actions["Reload"];
        switchWeaponAction = playerInput.actions["SwitchWeapon"];

        // Subscribe to input events
        fireAction.performed += OnFirePerformed;
        reloadAction.performed += OnReloadPerformed;
        switchWeaponAction.performed += OnSwitchWeaponPerformed;
    }

    private void Update()
    {
        // ONLY mobile controls - no PC shit
        // Auto-fire for AK47 when fire button is held
        if (IsAutomatic && fireAction.ReadValue<float>() > 0.1f && _currentWeapon != null)
        {
            _currentWeapon.Fire();
        }
    }

    // Mobile Input Events
    private void OnFirePerformed(InputAction.CallbackContext context)
    {
        // Single fire for pistol/shotgun
        if (!IsAutomatic && _currentWeapon != null)
        {
            _currentWeapon.Fire();
        }
    }

    private void OnReloadPerformed(InputAction.CallbackContext context)
    {
        ReloadCurrentWeapon();
    }

    private void OnSwitchWeaponPerformed(InputAction.CallbackContext context)
    {
        CycleWeapon();
    }

    private void CycleWeapon()
    {
        int newIndex = currentWeaponIndex;

        // Cycle through available weapons
        do
        {
            newIndex = (newIndex + 1) % weapons.Length;

            // Skip AK47 if not unlocked
            if (newIndex == 2 && !HasAK47)
            {
                continue;
            }

            break;

        } while (newIndex != currentWeaponIndex);

        SwitchWeapon(newIndex);
    }

    private void ReloadCurrentWeapon()
    {
        _currentWeapon?.Reload();
    }

    public void SwitchWeapon(int newIndex)
    {
        //validate index;
        if (newIndex < 0 || newIndex >= weapons.Length)
            return;

        //disable current weapon
        if (_currentWeapon != null)
        {
            _currentWeapon.gameObject.SetActive(false);
            DisableAllImage();
        }

        //enable new weapon
        currentWeaponIndex = newIndex;
        _currentWeapon = weapons[newIndex];
        _currentWeapon.gameObject.SetActive(true);

        // Set automatic flag based on weapon
        IsAutomatic = (newIndex == 2); // AK47 is automatic

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

    public static void GetAK47()
    {
        HasAK47 = true;
    }

    private void OnDestroy()
    {
        // Unsubscribe from events
        if (fireAction != null)
            fireAction.performed -= OnFirePerformed;
        if (reloadAction != null)
            reloadAction.performed -= OnReloadPerformed;
        if (switchWeaponAction != null)
            switchWeaponAction.performed -= OnSwitchWeaponPerformed;
    }
}