using UnityEngine;

public class SpawnDoor : MonoBehaviour
{
    [SerializeField] private GameObject doorPrefab;
    [SerializeField] private Transform[] spawnPoints;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject door = GameObject.Instantiate(doorPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
            Debug.Log("doorspawned!!");
        }
    }
}