using System;
using UnityEngine;

public class OnDeadEffects : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _particleEffect;
    [SerializeField]
    ParticleSystem _particleEffect2;

    private void Start()
    {
        EnemyHealth.OnEnemyDead += PlayParticleEffects;
    }

    private void PlayParticleEffects(EnemyHealth health)
    {
        transform.position = health.transform.position;
        _particleEffect?.Play();
        _particleEffect2?.Play();
    }

    private void OnDestroy()
    {
        // Unsubscribe when this object is destroyed
        EnemyHealth.OnEnemyDead -= PlayParticleEffects;
    }
}
