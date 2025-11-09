using TMPro;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    public TextMeshProUGUI text;
    public PlayerHealth playerHealth;
    public GuidedRocket guidedRocket;

    public void OnSliderChanged(float value)
    {
        switch (value)
        {
            case 1:
                text.text = "Chicken";
                playerHealth.pistoldamage = 1f;
                playerHealth.sgdamage = 2f;
                guidedRocket.explosionDamage = 2f;
                break;
            case 2:
                text.text = "Mother Chicken";
                playerHealth.pistoldamage = 1.5f;
                playerHealth.sgdamage = 3f; 
                guidedRocket.explosionDamage = 3f;
                break;
            case 3:
                text.text = "Normal";
                playerHealth.pistoldamage = 2f;
                playerHealth.sgdamage = 5f;
                guidedRocket.explosionDamage = 5f;
                break;
            case 4:
                text.text = "Slightly Difficult";
                playerHealth.pistoldamage = 3f;
                playerHealth.sgdamage = 7f;
                guidedRocket.explosionDamage = 7f;
                break;
            case 5:
                text.text = "Souls Like";
                playerHealth.pistoldamage = 5f;
                playerHealth.sgdamage = 10f;
                guidedRocket.explosionDamage = 10f;
                break;
            default:
                text.text = "error!!";

                break;

        }
    }
    


    
}
