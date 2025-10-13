using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarUI : MonoBehaviour
{
    [SerializeField]
    private List <Image> _imageArray;

    [SerializeField]
    private PlayerHealth _playerHealth;


    // Update is called once per frame
    void Update()
    {

        UpdateHealthBras();
    }

    private void UpdateHealthBras()
    {
        float health = _playerHealth.getHealth();  //takes health value from PlayerHealth


        for ( int i = 0 ; i < _imageArray.Count; i++ )
        {
                _imageArray[i].gameObject.SetActive ((_imageArray.Count - i) <= health) ;  //enables or disables healthbars
        }


    }
}
