using UnityEngine;

public class GuidedRocket : MonoBehaviour
{
    [Header("Effects")]
    public ParticleSystem trailParticles;
    public ParticleSystem explosionParticles;
    public AudioClip explosionSound;

    [Header("Explosion Settings")]
    public float explosionRadius = 3f;
    public float explosionDamage;
    public LayerMask damageLayer;

    [Header("Guidance Settings")]
    public float speed = 8f;
    public float turnSpeed = 120f;
    public float predictionTime = 0.3f; 
    public float lifetime = 5f;

    private Transform target;
    private Rigidbody2D rb;
    private bool isActive = true;

    public void Initialize(Transform target)
    {
        this.target = target;
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        if (trailParticles != null) trailParticles.Play();

        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        if (!isActive || target == null) return;

        // Predict target movement slightly
        Vector2 predictedPosition = (Vector2)target.position;
        Rigidbody2D targetRb = target.GetComponent<Rigidbody2D>();
        if (targetRb != null)
            predictedPosition += targetRb.linearVelocity * predictionTime;

        // Rotate smoothly toward predicted position
        Vector2 dir = (predictedPosition - (Vector2)transform.position).normalized;
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float newAngle = Mathf.MoveTowardsAngle(rb.rotation, targetAngle, turnSpeed * Time.fixedDeltaTime);
        rb.rotation = newAngle;

        // Move forward
        rb.linearVelocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive || collision.isTrigger) return;
        Explode();
    }

    void Explode()
    {
        isActive = false;
        rb.linearVelocity = Vector2.zero;
        if (GetComponent<SpriteRenderer>()) GetComponent<SpriteRenderer>().enabled = false;
        if (trailParticles != null) trailParticles.Stop();
        if (GetComponent<Collider2D>()) GetComponent<Collider2D>().enabled = false;

        if (explosionParticles != null)
        {
            ParticleSystem exp = Instantiate(explosionParticles, transform.position, Quaternion.identity);
            Destroy(exp.gameObject, exp.main.duration);
        }

        if (explosionSound != null)
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageLayer);
        foreach (Collider2D hit in hits)
        {
            PlayerHealth health = hit.GetComponent<PlayerHealth>();
            if (health != null) health.TakeDamage(explosionDamage);
        }

        Destroy(gameObject, 2f);
    }
}
