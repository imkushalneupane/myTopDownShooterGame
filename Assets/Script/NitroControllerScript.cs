using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NitroControllerScript : MonoBehaviour
{
    public PlayerController _player;

    [SerializeField]
    private int _maxNitroPacks = 1;
    private int _currentNitroPacks;
    private int _refillTime = 3;
    private bool _isRefilling = false;

    public ParticleSystem boostParticle;
    public AudioSource boostAudio;

    public Slider NitroSlider;  //refrence to the Nitro Slider UI



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentNitroPacks = _maxNitroPacks;
        NitroSlider.maxValue = _maxNitroPacks;  //sets the upper limit of the silder 
        ShowNitrosAvailable();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && _currentNitroPacks > 0)
        {


            StartCoroutine(Boost());
            Debug.Log("Boost! Pressed!");
            boostParticle.Play();  //playing the particle effect
            boostAudio.Play();  //plays the boost audio

            if (!_isRefilling && _currentNitroPacks < _maxNitroPacks)
            {
                StartCoroutine(RefillNitro());
            }
        }

        
    }

    private IEnumerator Boost()
    {
        _currentNitroPacks--;
        _player.moveSpeed = 40f;
        Debug.Log("Boost");

        yield return new WaitForSeconds(.125f);
        _player.moveSpeed = 5f;
        yield return new WaitForSeconds(.125f);
        boostParticle.Stop();  //ending the particle effect
        ShowNitrosAvailable();

    }
    private IEnumerator RefillNitro()
    {
        _isRefilling = true;
        while (_currentNitroPacks < _maxNitroPacks)
        {
            yield return new WaitForSeconds(_refillTime);
            _currentNitroPacks++;
            ShowNitrosAvailable() ;
        }
        _isRefilling = false;
    }

    private void ShowNitrosAvailable()
    {
        NitroSlider.value = _currentNitroPacks;
    }

    public void OnCylinderPick()
    {
        _maxNitroPacks++;    
        NitroSlider.maxValue = _maxNitroPacks;
        StartCoroutine(RefillNitro());
    }

}
