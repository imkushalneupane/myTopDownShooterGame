using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth _player; //refrence to player health
    [SerializeField]
    private RawImage _image;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _image.gameObject.SetActive(false);
        _player.OnPlayerDead += ShowGameOver; //subscribe to the palyerdeath info
    }

    private void ShowGameOver()
    {
        _image.gameObject.SetActive(true);
        Debug.Log("GameOver !!");
    }
}
