using UnityEngine;
using System.Collections;
using UnityEngine.Android;
using UnityEngine.Playables;
using NUnit.Framework;
public class RespawnPoint : MonoBehaviour
{
    [SerializeField] KeyHolder _keyHolder;
    [SerializeField] NitroControllerScript _nitro;
    private Vector3 currentCheckpoint;
    private bool nitro = false;
    private int i = 0;
    [SerializeField] private GameObject NitroPack;
    private bool hasredkey = false;
    private bool hasbluekey = false;
    private bool hasgreenkey = false;
    void Start()
    {

        transform.position = GetLocation();
        nitro = getnitropack();

        if (nitro == true)
        {
            NitroPack.gameObject.SetActive(true);
        }

        i = getnitroCylinder();
        for (int j = 0; j < i; j++)
        {
            _nitro.OnCylinderPick();
        }
        getkey();
        if (hasredkey == true)
        {
            Debug.Log("Player has Red Key");
            _keyHolder.AddKey(Key.Keytype.Red);

        }
        if (hasbluekey == true)
        {
            Debug.Log("Player has Blue Key");
            _keyHolder.AddKey(Key.Keytype.Blue);

        }
        if (hasgreenkey == true)
        {
            Debug.Log("Player has Green Key");
            _keyHolder.AddKey(Key.Keytype.Green);
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

        if (collision.CompareTag("NitroCylinder"))
        {
            i++;
        }
        setnitroCylinder();
        if (collision.CompareTag("RedKey"))
        {
            hasredkey = true;
        }
        if (collision.CompareTag("BlueKey"))
        {
            hasbluekey = true;
        }
        if (collision.CompareTag("GreenKey"))
        {
            hasgreenkey = true;
        }
        setkey();

    }
    private void setkey()
    {
        PlayerPrefs.SetInt("RedKey", hasredkey ? 1 : 0);
        PlayerPrefs.SetInt("BlueKey", hasbluekey ? 1 : 0);
        PlayerPrefs.SetInt("GreenKey", hasgreenkey ? 1 : 0);
        PlayerPrefs.Save();
    }
    private void getkey()
    {
        int redvalue = PlayerPrefs.GetInt("RedKey", 0);
        hasredkey = redvalue == 1;

        int bluevalue = PlayerPrefs.GetInt("BlueKey", 0);
        hasbluekey = bluevalue == 1;

        int greenvalue = PlayerPrefs.GetInt("GreenKey", 0);
        hasgreenkey = greenvalue == 1;
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
