using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public WeaponController weaponController;  //refrence to the WeaponController Scrpit

    Vector2 moveDirection;
    Vector2 mousePosition;

    [SerializeField]PlayerHealth _player;  //refrence to PlayerHealth script
    Renderer _renderer; //refrence to renderer

    private bool canBoost = true;


    private void Start()
    {
        _player = GetComponent<PlayerHealth>();
        _player.OnPlayerDead += OnDied;
        _renderer = GetComponent<Renderer>();
        
    }

    private void OnDied()
    {
        moveSpeed = 0f;
        _renderer.material.color = Color.grey;
    }





    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized; //assinging the moveDirection of the Player (up down left right)

        if (Input.GetKeyDown(KeyCode.Space) && canBoost)
        {

            StartCoroutine(Boost());
            Debug.Log("Boost! Pressed!");
        }
        

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);  //position of mouse on screen
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed; //Inplementing Actual Movement
          

        Vector2 aimDriection = mousePosition - rb.position; 
        float aimAngle = Mathf.Atan2(aimDriection.y , aimDriection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    
        
    }

    IEnumerator  Boost()
    {
        canBoost = false;
        moveSpeed = 40f;
        Debug.Log("Boost");

        yield return new WaitForSeconds(.125f);
        moveSpeed = 5f;

        yield return new WaitForSeconds(2.5f);
        canBoost = true;

    }

}
