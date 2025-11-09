using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public GameObject _deadboss;
    public Slider HealthSlider;
/*    public Slider EaseHealthSlider;
    private float lerpSpeed = 0.05f; */

    public static Action OnEnemyDead; //declaring the event

    

    [SerializeField] float _enemyHealth = 5f;
    public float _enemyCurrentHealth;

    [SerializeField] ParticleSystem _boom;

   



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemyCurrentHealth = _enemyHealth;

        if (HealthSlider!= null)
        HealthSlider.maxValue = _enemyHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) //for pistol bullets
        {
            _enemyCurrentHealth -= 2f;
            Debug.Log("shot!!");
        }
        if (collision.gameObject.CompareTag("SGBullet")) //for shotgun bullets
        {
            _enemyCurrentHealth -= 5f;
            Debug.Log("SG shot!!");
        }


        if (_enemyCurrentHealth <= 0f)
        {
            EnemyDie();
        }

        if (HealthSlider != null)
        {
            if (HealthSlider.value != _enemyHealth)
            {
                HealthSlider.value = _enemyCurrentHealth;
            }

            /*  if(HealthSlider.value != EaseHealthSlider.value) 
       {
           EaseHealthSlider.value = Mathf.Lerp(EaseHealthSlider.value, _enemyCurrentHealth, lerpSpeed);
       }*/
        }
    }



    private void EnemyDie()
    {
        OnEnemyDead?.Invoke();
       
         GameObject deadboss = Instantiate(_deadboss, transform.position, Quaternion.identity);

       

        Destroy(gameObject);
        HealthSlider.gameObject.SetActive(false);


    }
}
