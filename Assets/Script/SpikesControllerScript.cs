using System;
using System.Collections;
using UnityEngine;

public class SpikesControllerScript : MonoBehaviour
{
    //references to animator components
    [SerializeField]
    private Animator _spikeUp;
    [SerializeField]
    private Animator _spikeDown;

    public void OnPressed()
    {
        StartCoroutine(OnPressurePlatePressed());
    }

    private IEnumerator OnPressurePlatePressed()
    {
        _spikeUp.SetBool("PressureOn",true);
        _spikeDown.SetBool("PressureOn", true);

        yield return new WaitForSeconds(5f);
        _spikeUp.SetBool("PressureOn", false);
        _spikeDown.SetBool("PressureOn", false);
    }
}
