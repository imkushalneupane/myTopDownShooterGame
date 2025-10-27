using System;
using UnityEngine;
using UnityEngine.UI;

public class DropConsume : MonoBehaviour
{
    [SerializeField] PotionInfoScript health;
    [SerializeField] Pistol pistol;
    [SerializeField] Shortgun shortgun;
    [SerializeField] AK47 Ak47;

    [SerializeField] AudioSource  pickupAudio;

    [SerializeField] private Image messageImage;
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HealthPotion"))
        {
            health.GetHealthPotionDrop();
            pickupAudio.Play();
            Debug.Log("PotionAquired");
        } 
        else if (collision.gameObject.CompareTag("PistolDrop"))
        {
            pistol.GetPistolDrop();
            pickupAudio.Play();
        }
        else if (collision.gameObject.CompareTag("ShotgunDrop"))
        {
            shortgun.GetShotgunDrop();    
            pickupAudio.Play();
        }
        else if (collision.gameObject.CompareTag("AK47Drop"))
        {
            Ak47.GetAK47Drop();
            pickupAudio.Play();
        }

        else if (collision.gameObject.CompareTag("AK47"))
        {
            WeaponController.HasAK47 = true;
            ShowMessage();
        }

    }

    private void ShowMessage()
    {
        Time.timeScale = 0f;
        messageImage.gameObject.SetActive(true);
    }

    public void OnXPressed()
    {
        Time.timeScale = 1f;
    }
}
