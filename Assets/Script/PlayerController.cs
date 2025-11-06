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
    private InputAction mousePositionAction;

    // Joystick References (to disable on death)
    [Header("Mobile Joysticks")]
    [SerializeField] private OnScreenStick leftJoystick;
    [SerializeField] private OnScreenStick rightJoystick;

    // Reference to main camera for mouse position conversion
    private Camera mainCamera;

    private void Start()
    {
        _player = GetComponent<PlayerHealth>();
        _player.OnPlayerDead += OnDied;
        _renderer = GetComponent<Renderer>();
        mainCamera = Camera.main;

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

        // Handle aiming - check both methods
        HandleAiming();
    }

    private void HandleAiming()
    {
        //  Check for mouse input first
        Vector2 mousePos = aimAction.ReadValue<Vector2>();
        if (mousePos != Vector2.zero)
        {
            // Convert mouse screen position to world position
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCamera.nearClipPlane));
            aimDirection = (mouseWorldPos - transform.position).normalized;
            return;
        }

        //  Check for gamepad/joystick input
        Vector2 currentAim = aimAction.ReadValue<Vector2>();
        if (currentAim.magnitude > 0.3f) // Deadzone
        {
            aimDirection = currentAim.normalized;
        }
        // If neither has input, aimDirection remains the same as last frame
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