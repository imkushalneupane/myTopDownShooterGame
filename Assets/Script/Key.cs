using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Key: MonoBehaviour
{
    [SerializeField] private Keytype KeyType;

    public enum Keytype
    {
        Red,
        Blue,
        Green
    }
    public Keytype GetKeyType()
    {
        return KeyType;
        
    }
}
