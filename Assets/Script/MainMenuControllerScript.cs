using System.Collections;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControllerScript : MonoBehaviour
{

    [SerializeField]
    private Animator _animator;

    private void Start()
    {
        Time.timeScale = 1;
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
}
