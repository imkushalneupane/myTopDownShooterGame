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
                DifficultyManager.difficultyLevel = 1;
                break;
            case 2:
                text.text = "Mother Chicken";
                DifficultyManager.difficultyLevel = 2;
                break;
            case 3:
                text.text = "Normal";
                DifficultyManager.difficultyLevel = 3;
                break;
            case 4:
                text.text = "Slightly Difficult";
                DifficultyManager.difficultyLevel = 4;
                break;
            case 5:
                text.text = "Souls Like";
                DifficultyManager.difficultyLevel = 5;
                break;
            default:
                text.text = "error!!";

                break;

        }
    }
    


    
}
