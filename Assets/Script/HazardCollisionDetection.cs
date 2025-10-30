using System;
using UnityEngine;

public class HazardCollisionDetection : MonoBehaviour
{
    private PlayerHealth _playerHealth;

    [SerializeField]
    private SpikesControllerScript _spikeController;

    private void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes"))
        {
            _playerHealth.PlayerDeath();
        }

        if (collision.gameObject.CompareTag("PressurePlate"))
        {
            _spikeController.OnPressed();
        }

    }

    
}
