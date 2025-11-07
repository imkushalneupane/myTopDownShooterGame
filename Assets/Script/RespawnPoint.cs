using UnityEngine;
using System.Collections;
using UnityEngine.Android;
public class RespawnPoint : MonoBehaviour
{
    private Vector3 currentCheckpoint;

    
  
    void Start()
    {

     transform.position = GetLocation();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            currentCheckpoint = transform.position;
        }
        SetLocation();
    }

    private void SetLocation()
    {
        PlayerPrefs.SetFloat("X", currentCheckpoint.x );
        PlayerPrefs.SetFloat("Y", currentCheckpoint.y);
        PlayerPrefs.SetFloat("Z", currentCheckpoint.z);
        PlayerPrefs.Save();
    }  

    private Vector3 GetLocation()
    {
        float x = PlayerPrefs.GetFloat("X" , -71);
        float y = PlayerPrefs.GetFloat("Y", -48);
        float z = PlayerPrefs.GetFloat("Z", 0); 


      return new Vector3(x,y,z);
    }

   


}
