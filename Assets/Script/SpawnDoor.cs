using UnityEngine;

public class SpawnDoor : MonoBehaviour
{
    [SerializeField] private GameObject doorPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private EnemySpwannerScript enemySpawner; 

    [SerializeField] GameObject killCount;
    [SerializeField] GameObject pitfallMessage;

    

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject door = Instantiate(doorPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        Debug.Log("Door spawned!!");

        if (enemySpawner != null)
        {
            enemySpawner.playerEnters();
            Debug.Log("Enemies are spawned");
        }
        else
        {
            Debug.LogWarning("EnemySpawner reference not assigned!");
        }

        killCount.SetActive(true);
        enemySpawner.IsPlayerInPit = true;
        pitfallMessage.SetActive(true);
        Time.timeScale = 0;


       
    }

    public void OnXPressed()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}