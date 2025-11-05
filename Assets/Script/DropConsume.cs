using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DropConsume : MonoBehaviour
{
    [SerializeField] PotionInfoScript health;
    [SerializeField] Pistol pistol;
    [SerializeField] Shortgun shortgun;
    [SerializeField] AK47 Ak47;
    [SerializeField] NitroControllerScript _nitro;


    [SerializeField] AudioSource  pickupAudio;

    [SerializeField] private Image AKMessageImage;
    [SerializeField] private Image nitroPackMessageImage;
    [SerializeField] private Image nitroCylinderMessageImage;  

    [SerializeField] private GameObject NitroPack;



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
            ShowAKMessage();
            pickupAudio.Play();
        }
        else if (collision.gameObject.CompareTag("NitroPack"))
        {
            NitroPack.gameObject.SetActive(true);
            ShowNitroPackMessage();
            pickupAudio.Play();
        }
        else if (collision.gameObject.CompareTag("NitroCylinder"))
        {
            if (_nitro!= null)
            {
                _nitro.OnCylinderPick();
                ShowNitroCylinderMessage();
            }
            pickupAudio.Play();
           
        }


        else if (collision.gameObject.CompareTag("Portal"))
        {
            StartCoroutine(TeleportPlayer());
        }

    }

    private IEnumerator TeleportPlayer()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("boss_fight");
    }

    private void ShowAKMessage()
    {
        Time.timeScale = 0f;
        AKMessageImage.gameObject.SetActive(true);
    }
    private void ShowNitroPackMessage()
    {
        Time.timeScale = 0f;
        nitroPackMessageImage.gameObject.SetActive(true);
    }
    private void ShowNitroCylinderMessage()
    {
        Time.timeScale = 0f;
        nitroCylinderMessageImage.gameObject.SetActive(true);
    }

    public void OnXPressed()
    {
        Time.timeScale = 1f;
    }
}
