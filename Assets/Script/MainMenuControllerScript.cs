using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControllerScript : MonoBehaviour
{

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void OnPlayPressed()
    {
        SceneManager.LoadScene("Game1");
        Time.timeScale = 1f;
        Debug.Log("Loading Scene Game1");
    }
    public void OnExitPressed()
    {
        Application.Quit();
        Debug.Log("I quit!!");
    }

}
