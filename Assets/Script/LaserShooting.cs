using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class LaserShooting : MonoBehaviour
{
    [Header("References")]
    public Transform firePoint;
    public PlayerHealth playerHealth;
    public LayerMask playerLayer;       // Only the player
    public LayerMask obstacleLayer;     // Walls / obstacles

    [Header("Laser Settings")]
    public float laserRange = 20f;
    public float chargeTime = 1.5f;    
    public float beamDuration = 1.5f;  
    public float laserDamagePerSecond = 50f;
    public float cooldownTime = 2f;
    public float laserWidth = 0.2f;
    public float aimSpeed = 8f;
    public float flickerSpeed = 20f;
    public float glowPulseSpeed = 5f;  // smooth glow

    [Header("Colors")]
    public Color chargeMinColor = Color.yellow;
    public Color chargeMaxColor = Color.red;
    public Color fireColor = Color.red;

    private LineRenderer lr;
    private Material laserMat;
    private Transform playerTransform;
    private bool isFiring = false;
    private float cooldownTimer = 0f;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

        // LineRenderer setup
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.useWorldSpace = true;
        lr.enabled = false;

        // Glow material
        laserMat = new Material(Shader.Find("Unlit/Color"));
        lr.material = laserMat;
        lr.startWidth = laserWidth;
        lr.endWidth = laserWidth;

        lr.sortingLayerName = "UI";
        lr.sortingOrder = 999;
    }

    void Update()
    {
        if (playerTransform == null) return;

        // Cooldown
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            return;
        }

        if (!isFiring)
        {
            float distance = Vector2.Distance(transform.position, playerTransform.position);
            if (distance <= 15f) // detection range
            {
                StartCoroutine(LaserSequence());
                cooldownTimer = cooldownTime;
            }
        }

        // Auto-aim at player
        Vector2 dir = (playerTransform.position - firePoint.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Lerp(firePoint.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * aimSpeed);
    }

    private IEnumerator LaserSequence()
    {
        isFiring = true;

        // --- CHARGE PHASE ---
        float timer = 0f;
        lr.enabled = true;

        while (timer < chargeTime)
        {
            timer += Time.deltaTime;

            // F3 color flicker
            float flicker = Mathf.PingPong(Time.time * flickerSpeed, 1f);
            Color flickerColor = Color.Lerp(chargeMinColor, chargeMaxColor, flicker);

            // Glow pulse
            float pulse = (Mathf.Sin(Time.time * glowPulseSpeed) + 1f) * 0.5f;
            laserMat.color = flickerColor * Mathf.Lerp(0.5f, 2f, pulse);

            // Laser preview line to max distance (no damage yet)
            Vector3 startPos = firePoint.position; startPos.z = -5f;
            Vector3 endPos = firePoint.position + firePoint.right * laserRange; endPos.z = -5f;
            lr.SetPosition(0, startPos);
            lr.SetPosition(1, endPos);

            yield return null;
        }

        // --- FIRE PHASE ---
        laserMat.color = fireColor;
        timer = 0f;

        while (timer < beamDuration)
        {
            timer += Time.deltaTime;

            Vector3 startPos = firePoint.position; startPos.z = -5f;

            // Raycast to detect player and obstacles
            int combinedMask = playerLayer | obstacleLayer;
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right, laserRange, combinedMask);
            Vector3 endPos;

            if (hit)
            {
                endPos = hit.point;

                // Damage only if player
                PlayerHealth ph = hit.collider.GetComponent<PlayerHealth>();
                if (ph != null)
                {
                    ph.TakeDamage(laserDamagePerSecond * Time.deltaTime);
                }
            }
            else
            {
                endPos = firePoint.position + firePoint.right * laserRange;
            }

            endPos.z = -5f;
            lr.SetPosition(0, startPos);
            lr.SetPosition(1, endPos);

            yield return null;
        }

        // --- END ---
        lr.enabled = false;
        isFiring = false;
    }
}
