using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class KeyDoor : MonoBehaviour
{
    [SerializeField] private Key.Keytype keyType;
    public Key.Keytype GetKeyType()
    {
        return keyType;
    }
    public void OpenDoor()
    {
        Destroy(gameObject);

    }
}
