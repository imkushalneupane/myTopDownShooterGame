using System.Collections;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public GameObject shotgunbulletPrefab;
    public Transform firing_position;
    public Transform secondary_firing_position;
    public float bulletSpeed = 50f;
    public float fireRate = 2f;
    public float bulletLifeTime = 5f;
    public bool canFire = true;

    public AudioSource bulletSound;
    public ParticleSystem muzzleFlash;

    public void Shoot(Transform target)
    {
        primaryShoot(target);
        secondaryShoot(target);
    }
    public void primaryShoot(Transform target)
    {
        if (!canFire) return;
        if (bulletPrefab == null || firing_position == null || target == null) return;
        GameObject bullet = Instantiate(bulletPrefab, firing_position.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bulletSound.Play();
        muzzleFlash.Play();

        if (rb != null)
        {
            Vector2 direction = (target.position - firing_position.position).normalized;
            rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);

        }
        Destroy(bullet, bulletLifeTime);
        if (fireRate > 0f)
        {
            StartCoroutine(CoolDown());
        }

    }
        private IEnumerator CoolDown()
    {
        canFire = false;
        yield return new WaitForSeconds(fireRate);
        canFire = true;

    }

     public void secondaryShoot(Transform target)
    {
        if (!canFire) return;
        if (shotgunbulletPrefab == null || secondary_firing_position == null || target == null) return;
        GameObject bullet2 = Instantiate(shotgunbulletPrefab, secondary_firing_position.position, Quaternion.identity);
        Rigidbody2D rb = bullet2.GetComponent<Rigidbody2D>();
        bulletSound.Play();
        muzzleFlash.Play();

        if (rb != null)
        {
            Vector2 direction = (target.position - secondary_firing_position.position).normalized;
            rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);

        }
        Destroy(bullet2, bulletLifeTime);
        if (fireRate > 0f)
        {
            StartCoroutine(CoolDown2());
        }

    }
    private IEnumerator CoolDown2()
    {
        canFire = false;
        yield return new WaitForSeconds(fireRate);
        canFire = true;

    }
        
    

    
}
