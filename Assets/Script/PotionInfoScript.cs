using TMPro;
using UnityEngine;

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

    private void Start()
    {
        ShowPotioInfo();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _potionCount > 0)
        {
            _health.Regenerate();   //regeneration via PlayerHealth class.
            _potionCount--;
            ShowPotioInfo();
            healAudio.Play();
        }
        
    }

    private void ShowPotioInfo()   //shows no of potions available
    {
        _potionInfo.text = _potionCount.ToString();
    }

    public void GetHealthPotionDrop()  //gets drop from drop consume class
    {
        if (_potionCount < maxPotionCapacity)
        {
            _potionCount++;
            ShowPotioInfo();
        }
    }



}
