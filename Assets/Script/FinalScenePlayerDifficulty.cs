using UnityEngine;

public class FinalScenePlayerDifficulty : MonoBehaviour
{
    public PlayerHealth playerHealth;
    //public GuidedRocket guidedRocket;
    void Start()
    {
            int diff = DifficultyManager.difficultyLevel;

        if (diff == 1)
        {
            playerHealth.pistoldamage = 1f;
            playerHealth.sgdamage = 2f;
           // guidedRocket.explosionDamage = 2f;
        }
        else if (diff == 2)
        {


            playerHealth.pistoldamage = 1.5f;
            playerHealth.sgdamage = 3f;
           // guidedRocket.explosionDamage = 3f;
        }
        else if (diff == 3)
        {

            playerHealth.pistoldamage = 2f;
            playerHealth.sgdamage = 5f;
           // guidedRocket.explosionDamage = 5f;
        }else if (diff == 4)
        {
                            playerHealth.pistoldamage = 3f;
                playerHealth.sgdamage = 7f;
                //guidedRocket.explosionDamage = 7f;
        }else if (diff == 5)
        {
                playerHealth.pistoldamage = 5f;
                playerHealth.sgdamage = 10f;
               /* guidedRocket.explosionDamage = 10f; */
        }
        

    
            
            
  
        
   
    }

 
}
