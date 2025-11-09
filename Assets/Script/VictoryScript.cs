using System;
using System.Collections;
using UnityEngine;

public class VictoryScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _victoryImage;
    [SerializeField]
    private AudioSource _bgAudio;
    [SerializeField]
    private AudioSource _victoryAudio;
    [SerializeField]
    private GameObject _mainMenuButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _victoryImage.SetActive(false); 
        _mainMenuButton.SetActive(false);
        BossHealth.OnEnemyDead += ShowVictory;
    }

    public void ShowVictory()
    {
        StartCoroutine(OnVictory());

       _victoryImage.SetActive(true);
        

    }

    IEnumerator OnVictory()
    {
        yield return new WaitForSeconds(2);
        _bgAudio.Stop();
        yield return new WaitForSeconds(.5f);
        _victoryImage.SetActive(true);
        _victoryAudio.Play();
        yield return new WaitForSeconds(3.5f);
        _mainMenuButton.SetActive(true);
    }
}
