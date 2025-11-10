using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class KeyDoor : MonoBehaviour
{
    [SerializeField] private Key.Keytype keyType;
    [SerializeField] private AudioSource _doorBreakAudio;



    public Key.Keytype GetKeyType()
    {
        return keyType;
    }
    public void OpenDoor()
    {
        _doorBreakAudio.Play();
        
        StartCoroutine(OnDoorDestroy());

    }

    IEnumerator OnDoorDestroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
