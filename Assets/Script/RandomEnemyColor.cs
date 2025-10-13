using UnityEngine;

public class RandomEnemyColor : MonoBehaviour
{
    public Color[] possibleColors = new Color[]
    {
        Color.lightGreen,
        Color.lightBlue,
        Color.lightGray
         
        
        
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get renderer component
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            //pick a random color from array
            Color randomColor = possibleColors[Random.Range(0, possibleColors.Length)];

            //Apply to the color to the material
            renderer.material.color = randomColor;
        }

        
    }

    
}
