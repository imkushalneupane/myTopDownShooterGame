using UnityEngine;

public class DropConsume : MonoBehaviour
{
    [SerializeField] PlayerHealth health;
    [SerializeField] Pistol pistol;
    [SerializeField] Shortgun shortgun;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HealthPotion"))
        {
            health.GetHealthPotionDrop();
        } 
        else if (collision.gameObject.CompareTag("PistolDrop"))
        {
            pistol.GetPistolDrop();
        }
        else if (collision.gameObject.CompareTag("ShotgunDrop"))
        {
            shortgun.GetShotgunDrop();    
        }
    }

    
}
