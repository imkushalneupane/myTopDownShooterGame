using UnityEngine;

public class DiifficultyPoint : MonoBehaviour
{
    private int savedDiff = DifficultyManager.difficultyLevel;
    public PlayerHealth playerHealth;
    void Start()
    {
        if (GetDifficulty() == 1)
        {
            playerHealth.pistoldamage = 1f;
            playerHealth.sgdamage = 2f;

        }
        else if (GetDifficulty() == 2)
        {


            playerHealth.pistoldamage = 1.5f;
            playerHealth.sgdamage = 3f;

        }
        else if (GetDifficulty() == 3)
        {

            playerHealth.pistoldamage = 2f;
            playerHealth.sgdamage = 5f;

        }
        else if (GetDifficulty() == 4)
        {
            playerHealth.pistoldamage = 3f;
            playerHealth.sgdamage = 7f;

        }
        else if (GetDifficulty() == 5)
        {
            playerHealth.pistoldamage = 5f;
            playerHealth.sgdamage = 10f;

        }
        DifficultyManager.diffi= GetDifficulty();


    }
    public void SetDifficulty()
    {
        PlayerPrefs.SetInt("D", savedDiff);
        PlayerPrefs.Save();
    }
    public int GetDifficulty()
    {
        return PlayerPrefs.GetInt("D", 3);

    }



}
