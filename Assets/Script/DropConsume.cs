using UnityEngine;

public class DropConsume : MonoBehaviour
{
    [SerializeField] PlayerHealth health;
    [SerializeField] Pistol pistol;
    [SerializeField] Shortgun shortgun;

    [SerializeField] AudioSource  healAudio;
    [SerializeField] AudioSource  AmmoPickupAudio;




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HealthPotion"))
        {
            health.GetHealthPotionDrop();
            healAudio.Play();
        } 
        else if (collision.gameObject.CompareTag("PistolDrop"))
        {
            pistol.GetPistolDrop();
            AmmoPickupAudio.Play();
        }
        else if (collision.gameObject.CompareTag("ShotgunDrop"))
        {
            shortgun.GetShotgunDrop();    
            AmmoPickupAudio.Play();
        }
    }

    
}
