using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuControllerScript : MonoBehaviour
{
   public void OnPausePressed()
    {
        Time.timeScale = 0f; //pauses game
    }

    public void OnContinuePressed()
    {
        Time.timeScale = 1f; //resumes game
    }

    public void OnRestartPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  //restarts the game level
        Time.timeScale = 1f; //resumes game
    } 

    public void OnMainMenuPressed()
    {
        SceneManager.LoadScene("MainMenu");  //loads mainmenu
    }
}
