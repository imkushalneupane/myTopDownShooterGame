using UnityEngine;

public class PlayerDifficultyHandler : MonoBehaviour
{
    public PlayerHealth playerHealth;
    
    void Start()
    {
            int diff = DifficultyManager.difficultyLevel;

        if (diff == 1)
        {
            playerHealth.pistoldamage = 1f;
            playerHealth.sgdamage = 2f;
        
        }
        else if (diff == 2)
        {


            playerHealth.pistoldamage = 1.5f;
            playerHealth.sgdamage = 3f;
         
        }
        else if (diff == 3)
        {

            playerHealth.pistoldamage = 2f;
            playerHealth.sgdamage = 5f;
      
        }else if (diff == 4)
        {
                            playerHealth.pistoldamage = 3f;
                playerHealth.sgdamage = 7f;
          
        }else if (diff == 5)
        {
                playerHealth.pistoldamage = 5f;
                playerHealth.sgdamage = 10f;
        
        }
        

    
            
            
  
        
   
    }

 
}
