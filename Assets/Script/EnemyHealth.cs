using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public static Action<Transform> OnEnemyDead; //declaring the event

    [SerializeField] float _enemyHealth = 5f;
    private float _enemyCurrentHealth;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemyCurrentHealth = _enemyHealth;
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


        if(_enemyCurrentHealth <= 0f)
        {
            EnemyDie();
        }
    }

    private void EnemyDie()
    {

        if (OnEnemyDead != null)
        {
            OnEnemyDead.Invoke(transform); // Publishing the event
            Debug.Log("Enemy died and published death event");
        }


        Debug.Log("EnemyDead");

        Destroy(gameObject);

        
    }
}
