using UnityEngine;

public class SpawnDoor : MonoBehaviour
{
    [SerializeField] private GameObject doorPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private EnemySpwannerScript enemySpawner; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject door = Instantiate(doorPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
            Debug.Log("Door spawned!!");

            if(enemySpawner != null)
            {
                enemySpawner.playerEnters();
                Debug.Log("Enemies are spawned");
            }
            else
            {
                Debug.LogWarning("EnemySpawner reference not assigned!");
            }
        }
    }
}