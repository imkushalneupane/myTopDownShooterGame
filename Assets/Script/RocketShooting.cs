using UnityEngine;

public class RocketShooting : MonoBehaviour
{
    [Header("Rocket Settings")]
    public GameObject rocketPrefab;
    public Transform firePoint;
    public Transform target;

    [Header("Shooting Settings")]
    public float fireCooldown = 5f;
    public float rocketSpeed = 8f;
    public float turnSpeed = 120f;
    public float predictionTime = 0.5f;
    public float avoidanceForce = 3f;
    public float avoidanceDistance = 2.5f;
    public float rocketLifetime = 6f;
    public LayerMask obstacleLayer;

    [Header("Effects")]
    public ParticleSystem muzzleFlash;
    public AudioClip shootSound;
    private AudioSource audioSource;

    private float nextFireTime = 0f;

    void Start()
    {
        // Setup Audio Source
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1f;
        }
    }

    public void TryShootRocket()
    {
        if (Time.time < nextFireTime) return; // Cooldown check
        if (rocketPrefab == null || firePoint == null || target == null) return;

        // Spawn rocket
        GameObject rocket = Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();
        if (rb == null) rb = rocket.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        // Setup rocket script
        GuidedRocket guided = rocket.GetComponent<GuidedRocket>();
        if (guided == null) guided = rocket.AddComponent<GuidedRocket>();

guided.Initialize(target);


        // Initial velocity
        rb.linearVelocity = firePoint.right * rocketSpeed;

        // Effects
        if (muzzleFlash != null) muzzleFlash.Play();
        if (shootSound != null && audioSource != null)
            audioSource.PlayOneShot(shootSound);

        // Start cooldown
        nextFireTime = Time.time + fireCooldown;
    }

    public bool CanShoot()
    {
        return Time.time >= nextFireTime;
    }
}
