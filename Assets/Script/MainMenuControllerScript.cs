using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuControllerScript : MonoBehaviour
{
    [SerializeField]
    private Button _continueButton;
   

    private void Start()
    {
        Time.timeScale = 1;

        CheckFirstTimeLoading();
    }

    public void OnPlayPressed()
    {
        SceneManager.LoadScene("Game1");
        Time.timeScale = 1f;

        // StartCoroutine(OnContinuePressed()); 
    }
    public void OnExitPressed()
    {
        Application.Quit();
        Debug.Log("I quit!!");
    }

    /*
    private IEnumerator OnContinuePressed()
    {
        _animator.SetTrigger("SceneTransition");
        yield return  new WaitForSeconds(.5f);
        SceneManager.LoadScene("Game1");
        Time.timeScale = 1f;
    }
    */

    private void CheckFirstTimeLoading()
    {
        int x = PlayerPrefs.GetInt("CheckFirstNewGamePressed",0);
        if (x == 0)
        {
            _continueButton.gameObject.SetActive(false);
        }
        
    }

    public void OnNewGamePressed()
    {
        PlayerPrefs.SetInt("CheckFirstNewGamePressed",1);
        PlayerPrefs.Save();
    }
}
