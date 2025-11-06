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
    private InputAction fireAction;

    // Joystick References (to disable on death)
    [Header("Mobile Joysticks")]
    [SerializeField] private OnScreenStick leftJoystick;
    [SerializeField] private OnScreenStick rightJoystick;

    // Reference to main camera for mouse position conversion
    private Camera mainCamera;

    // Track active input method
    private enum InputMethod { Mouse, Gamepad }
    private InputMethod currentInputMethod = InputMethod.Mouse;
    private Vector2 lastGamepadAim = Vector2.up;
    private Vector2 lastMousePosition;

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
        mousePositionAction = playerInput.actions["MousePosition"];
        fireAction = playerInput.actions["Fire"]; 

        lastMousePosition = mousePositionAction.ReadValue<Vector2>();
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

        // Handle aiming
        HandleAiming();
    }

    private void HandleAiming()
    {
        Vector2 gamepadAim = aimAction.ReadValue<Vector2>();
        Vector2 currentMousePos = mousePositionAction.ReadValue<Vector2>();

        // Check if mouse has moved significantly
        bool mouseMoved = (currentMousePos - lastMousePosition).sqrMagnitude > 25f; // 5 pixel threshold

        // Check if fire button is pressed with mouse
        bool mouseFire = fireAction.triggered && mouseMoved;

        // If gamepad is being actively used, switch to gamepad
        if (gamepadAim.magnitude > 0.3f)
        {
            currentInputMethod = InputMethod.Gamepad;
            lastGamepadAim = gamepadAim;
            aimDirection = gamepadAim.normalized;
        }
        // If mouse is moved or fire button is pressed with mouse, switch to mouse
        else if (mouseMoved || mouseFire)
        {
            currentInputMethod = InputMethod.Mouse;
            aimDirection = GetMouseAimDirection();
        }
        // Otherwise maintain current input method
        else
        {
            if (currentInputMethod == InputMethod.Gamepad)
            {
                aimDirection = lastGamepadAim.normalized;
            }
            else
            {
                aimDirection = GetMouseAimDirection();
            }
        }

        lastMousePosition = currentMousePos;
    }

    private Vector2 GetMouseAimDirection()
    {
        Vector2 mousePos = mousePositionAction.ReadValue<Vector2>();
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCamera.nearClipPlane));
        return (mouseWorldPos - transform.position).normalized;
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