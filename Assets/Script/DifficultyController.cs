using TMPro;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    public TextMeshProUGUI text;
    
   
    public void OnSliderChanged(float value)
    {
        switch (value)
        {
            case 1:
                text.text = "Chicken";
                break;
            case 2:
                text.text = "Mother Chicken";
                break;
            case 3:
                text.text = "Normal";
                break;
            case 4:
                text.text = "Slightly Difficult";
                break;
            case 5:
                text.text = "Souls Like";
                break;
            default:
                text.text = "error!!";
                break;

        }
    }
}
