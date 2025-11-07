using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class NitroControllerScript : MonoBehaviour
{
    public PlayerController _player;

    [SerializeField]
    private int _maxNitroPacks = 1;
    private int _currentNitroPacks;
    private int _refillTime = 3;
    private bool _isRefilling = false;
    private bool _isBoosting = false; 

    public ParticleSystem boostParticle;
    public AudioSource boostAudio;

    public Slider NitroSlider;

    // Mobile Input
    [SerializeField]private PlayerInput playerInput;
    private InputAction nitroAction;

    void Start()
    {
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<UnityEngine.InputSystem.PlayerInput>();

        _currentNitroPacks = _maxNitroPacks;
        NitroSlider.maxValue = _maxNitroPacks;
        ShowNitrosAvailable();

        // Get mobile input
        
        nitroAction = playerInput.actions["Nitro"];

        nitroAction.performed += OnNitroPerformed;
    }

    

    // Mobile Input Event
    private void OnNitroPerformed(InputAction.CallbackContext context)
    {
        // Check if we can use nitro (has charges AND not already boosting)
        if (_currentNitroPacks > 0 && !_isBoosting)
        {
            StartCoroutine(Boost());
        }
    }

    private IEnumerator Boost()
    {
        _isBoosting = true; 
        _currentNitroPacks--;

        Debug.Log("Boost Activated! Nitro packs left: " + _currentNitroPacks);
        boostParticle.Play();
        boostAudio.Play();

        float originalSpeed = _player.moveSpeed;
        _player.moveSpeed = 40f;

        yield return new WaitForSeconds(0.125f);

        _player.moveSpeed = originalSpeed;
        boostParticle.Stop();

        ShowNitrosAvailable();

        // Start refilling if we have empty slots
        if (!_isRefilling && _currentNitroPacks < _maxNitroPacks)
        {
            StartCoroutine(RefillNitro());
        }

        _isBoosting = false; 
    }

    private IEnumerator RefillNitro()
    {
        _isRefilling = true;

        while (_currentNitroPacks < _maxNitroPacks)
        {
            yield return new WaitForSeconds(_refillTime);
            _currentNitroPacks++;
            Debug.Log("Nitro Refilled! Current: " + _currentNitroPacks);
            ShowNitrosAvailable();
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

        // If we're not already refilling, start refilling to use the new capacity
        if (!_isRefilling)
        {
            StartCoroutine(RefillNitro());
        }
    }

    public void OnGameReset()
    {
        _maxNitroPacks = 1;
    }

    private void OnDestroy()
    {
        if (nitroAction != null)
        {
            nitroAction.performed -= OnNitroPerformed;
        }
    }
}