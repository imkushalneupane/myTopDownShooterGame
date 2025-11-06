using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PotionInfoScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _potionInfo;
    [SerializeField]
    private PlayerHealth _health;
    [SerializeField]
    AudioSource healAudio;
    [SerializeField]
    int maxPotionCapacity = 4;
    [SerializeField]
    private int _potionCount = 2;

    // Mobile Input
    [SerializeField] private PlayerInput playerInput;
    private InputAction usePotionAction;

    private void Start()
    {
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<UnityEngine.InputSystem.PlayerInput>();

        ShowPotionInfo();

        // Get mobile input
        
        usePotionAction = playerInput.actions["Heal"];

        usePotionAction.performed += OnUsePotionPerformed;
    }

    // Mobile Input Event
    private void OnUsePotionPerformed(InputAction.CallbackContext context)
    {
        if (_potionCount > 0)
        {
            _health.Regenerate();   //regeneration via PlayerHealth class.
            _potionCount--;
            ShowPotionInfo();
            healAudio.Play();
        }
    }

    private void ShowPotionInfo()   //shows no of potions available
    {
        _potionInfo.text = _potionCount.ToString();
    }

    public void GetHealthPotionDrop()  //gets drop from drop consume class
    {
        if (_potionCount < maxPotionCapacity)
        {
            _potionCount++;
            ShowPotionInfo();
        }
    }

    private void OnDestroy()
    {
        if (usePotionAction != null)
        {
            usePotionAction.performed -= OnUsePotionPerformed;
        }
    }
}