using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class KeyHolder : MonoBehaviour
{
    private List<Key.Keytype> keyList;
    private void Awake()
    {
        keyList = new List<Key.Keytype>();

    }
    public void AddKey(Key.Keytype keytype)
    {
        Debug.Log("Added key: " + keytype);
        keyList.Add(keytype);
    }
    public void RemoveKey(Key.Keytype keytype)
    {
        keyList.Remove(keytype);
    }
    public bool HasKey(Key.Keytype keytype)
    {
        return keyList.Contains(keytype);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Key key = collider.GetComponent<Key>();
        if (key != null)
        {
            AddKey(key.GetKeyType());
            Destroy(key.gameObject);
        }
        KeyDoor keyDoor = collider.GetComponent<KeyDoor>();
        if(keyDoor != null)
        {
            if (HasKey(keyDoor.GetKeyType()))
            {
                RemoveKey(keyDoor.GetKeyType());
                keyDoor.OpenDoor();
            }
     
            
        }
    }

}
