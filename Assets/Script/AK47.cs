using TMPro;
using UnityEngine;

public class AK47 : Weapon
{
    public TextMeshProUGUI AK47Text;
    public TextMeshProUGUI AK47ReloadStatus;

    public ParticleSystem muzzleFlash;
    public AudioSource fireAudio;


    // Overriding to enable automatic fire
    protected override bool IsAutomatic => true;

    private void Start()
    {
        _totalBullet = 60f;
        _bulletOnMag = magazineCapacity;
        reloadTime = 3f;
    }

    private void Update()
    {
        AK47TextDisplay();
    }

    private void AK47TextDisplay()
    {
        AK47Text.text = _bulletOnMag.ToString() + "/" + _totalBullet;

        AK47ReloadStatus.text = isReloading ? "Reloading...!" : "Ready!";
    }

    public override void Fire()  //must be overridden
    {
        if (!CanFire() || isReloading)
            return;

        //Single bullet in the direction the firePoint is facing
        CreateBullet(firePoint.up);  //function call from the weaponController class

        //updating the timer
        nextFireTime = Time.time + fireRate;
        Debug.Log("AK47 Fired");

        //reducing the bullets
        _bulletOnMag--;

        //MuzzleFlash
        muzzleFlash.Play();
        //fire sound
        fireAudio.Play();


        //autoreload when magazing is empty
        if (_bulletOnMag <= 0 && _totalBullet > 0)
        {
            Reload();
        }
    }

    public void GetAK47Drop()
    {
        _totalBullet += 30f;
    }

}
