using JetBrains.Annotations;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Action OnPlayerDead;  //creating an event for player death

    private float _maxHealth = 10f;
    private float _currentHealth;
    private float damagePerHit = 1f;
    private float invincibilityTime = 1f;

    public TextMeshProUGUI healthBar;
    public Renderer _renderer;

    private float invincibleTimer;

    private bool isRegenerating = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _currentHealth = _maxHealth;
        UpdateHealthUI();
    }

    private void Update()
    {
        if (invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.F))
        {
            Regenerate();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && invincibleTimer <= 0)
        {
            TakeDamage(damagePerHit);
            invincibleTimer = invincibilityTime;
        }
        if (collision.gameObject.CompareTag("Enemy_pistolBullet"))
        {
            TakeDamage(2f);
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.CompareTag("Enemy_SGBullet"))
        {
            TakeDamage(5f);
            Destroy(collision.gameObject);
            
        }
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        _currentHealth = Mathf.Max( _currentHealth,0f);  //clam the _currnethealth between these valuse
        Debug.Log("Player Health: " + _currentHealth);
        UpdateHealthUI();

        if(_currentHealth <= 0f)
        {
            Debug.Log("Player Died");

            PlayerDie();

            //other death stuff
        }

    }

    private void PlayerDie()
    {
        OnPlayerDead?.Invoke(); //publishing that player died
    }

    private void UpdateHealthUI()
    {
        healthBar.text = _currentHealth.ToString();
    }

    private void Regenerate()
    {

        if (_currentHealth == _maxHealth || isRegenerating)
        {
            return;
        }

        StartCoroutine(RegenCoroutine());

    }

    private IEnumerator RegenCoroutine()
    {
        isRegenerating = true;
        int RegenPoints = 5;

        while (RegenPoints >= 0)
        {

            _renderer.material.color = Color.green;
            _currentHealth++;
            RegenPoints --;
            UpdateHealthUI();
            yield return new WaitForSeconds(.25f);
            _renderer.material.color = Color.white;

            if (_currentHealth >= 10f)  //if health is full , then break outta loop
                break;

            yield return new WaitForSeconds(.25f);
        }

        isRegenerating = false;
    }

    public void GetHealthPotionDrop()
    {
        Regenerate();
    }

    public float getHealth()  //to get health value from other calsses
    {
        return _currentHealth;
    }
}
