using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public WeaponController weaponController;  // reference to the WeaponController Script

    Vector2 moveDirection;
    Vector2 mousePosition;

    [SerializeField] PlayerHealth _player;  // reference to PlayerHealth script
    Renderer _renderer; // reference to renderer

    // Input System components
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction mousePositionAction;

    private void Start()
    {
        _player = GetComponent<PlayerHealth>();
        _player.OnPlayerDead += OnDied;
        _renderer = GetComponent<Renderer>();

        // Initialize Input System
        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            playerInput = gameObject.AddComponent<PlayerInput>();
        }

        // Get input actions
        moveAction = playerInput.actions["Move"];
        mousePositionAction = playerInput.actions["MousePosition"];
    }

    private void OnDied()
    {
        moveSpeed = 0f;
        _renderer.material.color = Color.grey;

        // Disable input when player dies
        if (playerInput != null)
        {
            playerInput.enabled = false;
        }
    }

    void Update()
    {
        // Get movement input from new Input System
        moveDirection = moveAction.ReadValue<Vector2>().normalized;

        // Get mouse position from new Input System
        mousePosition = Camera.main.ScreenToWorldPoint(mousePositionAction.ReadValue<Vector2>());
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed; // Implementing Actual Movement

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    
    private void OnDestroy()
    {
        if (_player != null)
        {
            _player.OnPlayerDead -= OnDied;
        }
    }
}