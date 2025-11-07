using UnityEngine;
using System.Collections;
using UnityEngine.Android;
using UnityEngine.Playables;
public class RespawnPoint : MonoBehaviour
{

    [SerializeField] NitroControllerScript _nitro;
    private Vector3 currentCheckpoint;
    private bool nitro = false;
    private int i = 0;
    [SerializeField] private GameObject NitroPack;
    void Start()
    {

        transform.position = GetLocation();
        nitro = getnitropack();

        if (nitro == true)
        {
            NitroPack.gameObject.SetActive(true);
        }

        i = getnitroCylinder();
        for(int j = 0; j < i; j++)
        {
            _nitro.OnCylinderPick();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            currentCheckpoint = transform.position;
        }
        SetLocation();
        if (collision.CompareTag("NitroPack"))
        {
            nitro = true;
        }
        setnitropack();
        
        if( collision.CompareTag("NitroCylinder"))
        {
            i++;
        }
        setnitroCylinder();

        
    }

    private void setnitropack()
    {
        PlayerPrefs.SetInt("NitroPack", nitro ? 1 : 0);
        PlayerPrefs.Save();

    }
    private bool getnitropack()
    {
        int value = PlayerPrefs.GetInt("NitroPack", 0);
        return value == 1;
    }
    private void setnitroCylinder()
    {
        PlayerPrefs.SetInt("NitroCylinder", i);
        PlayerPrefs.Save();
    }
    private int getnitroCylinder()
    {
        return PlayerPrefs.GetInt("NitroCylinder", 0);
    }
    private void SetLocation()
    {
        PlayerPrefs.SetFloat("X", currentCheckpoint.x);
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
