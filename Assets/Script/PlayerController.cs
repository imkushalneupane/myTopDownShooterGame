using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public WeaponController weaponController;

    Vector2 moveDirection;
    Vector2 aimDirection;

    [SerializeField] PlayerHealth _player;
    Renderer _renderer;

    // Input Actions
    private InputAction moveAction;
    private InputAction aimAction;

    // Joystick References (to disable on death)
    [Header("Mobile Joysticks")]
    [SerializeField] private OnScreenStick leftJoystick;
    [SerializeField] private OnScreenStick rightJoystick;

    private void Start()
    {
        _player = GetComponent<PlayerHealth>();
        _player.OnPlayerDead += OnDied;
        _renderer = GetComponent<Renderer>();

        // Get the Input Actions from the PlayerInput component
        PlayerInput playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        aimAction = playerInput.actions["Aim"];
    }

    private void OnDied()
    {
        moveSpeed = 0f;
        _renderer.material.color = Color.grey;

        // Disable joysticks on death
        if (leftJoystick != null) leftJoystick.enabled = false;
        if (rightJoystick != null) rightJoystick.enabled = false;
    }

    void Update()
    {
        // Movement
        moveDirection = moveAction.ReadValue<Vector2>().normalized;

        // Aim - only update if we have significant input
        Vector2 currentAim = aimAction.ReadValue<Vector2>();
        if (currentAim.magnitude > 0.3f) // Deadzone
        {
            aimDirection = currentAim;
        }
    }

    private void FixedUpdate()
    {
        // Movement
        rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;

        // Rotation - always use the last good aim direction
        if (aimDirection != Vector2.zero)
        {
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = aimAngle;
        }
    }

    private void OnDestroy()
    {
        if (_player != null)
        {
            _player.OnPlayerDead -= OnDied;
        }
    }
}