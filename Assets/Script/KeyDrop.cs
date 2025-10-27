using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class KeyDrop: MonoBehaviour
{
    [SerializeField] private GameObject keyPrefabs;
    private void OnEnable()
    {
        EnemyHealth.OnEnemyDead += DropKey;
    }
    private void DropKey(EnemyHealth deadEnemy)
    {
        if (deadEnemy.gameObject != gameObject) return;
        Instantiate(keyPrefabs, GetRandomizedPostion(transform.position), transform.rotation);
        Debug.Log("Dropped key ");
    }
    
    private Vector3 GetRandomizedPostion(Vector3 basePostion)
    {
        float randomX = UnityEngine.Random.Range(-.5f,.5f);
        float randomY = UnityEngine.Random.Range(-.5f, .5f);
        return basePostion + new Vector3(randomX , randomY ,0);
    }

 

    private void OnDisable()
    {
        EnemyHealth.OnEnemyDead -= DropKey;
    }


}
