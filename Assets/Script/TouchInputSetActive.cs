using UnityEngine;
using UnityEngine.UI;

public class TouchInputSetActive : MonoBehaviour
{
    public Toggle _touchInputToggle;
    public GameObject _touchInput;

    void Start()
    {
        // Load saved toggle state (default = on)
        bool savedState = PlayerPrefs.GetInt("TouchInputToggle", 1) == 1;

        // Apply to toggle and GameObject
        _touchInputToggle.isOn = savedState;
        _touchInput.SetActive(savedState);
    }

    public void OnTogglePressed()
    {
        bool isOn = _touchInputToggle.isOn;

        
        PlayerPrefs.SetInt("TouchInputToggle", isOn ? 1 : 0);
        PlayerPrefs.Save();

        
        _touchInput.SetActive(isOn);
    }
}
