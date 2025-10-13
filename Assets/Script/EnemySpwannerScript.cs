
using System.Collections;
using UnityEngine;

public class EnemySpwannerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _EnemyPrefab;

    public Transform enemySpawnner1;
    public Transform enemySpawnner2;
    public Transform enemySpawnner3;

    private int _enemiesAlive =0;

    


    private void Start()
    {
        EnemyHealth.OnEnemyDead += SpawnNewEnemyOnDeath; //event subscription
        SpawnInitialEnemies();

    }

    private void SpawnInitialEnemies()
    {
        SpawnEnemyAtSpawner(enemySpawnner1);
        SpawnEnemyAtSpawner(enemySpawnner2);
        SpawnEnemyAtSpawner(enemySpawnner3);
        _enemiesAlive = 3;
    }

    private void SpawnEnemyAtSpawner(Transform spawner)
    {
        GameObject newEnemy = Instantiate(_EnemyPrefab, spawner.position, Quaternion.identity);
        Debug.Log($"Spawned enemy at {spawner.name}");
    }

    private void SpawnNewEnemyOnDeath(Transform deathPosition)
    {
        _enemiesAlive--; //decrease the no. of active enimes

        //spawn a new enemy at random spawnner after a short delay
        StartCoroutine(SpawnNewEnemyWithDelay());
        
    }

    private IEnumerator SpawnNewEnemyWithDelay()
    {
        //wait for one second before spawning new enemy
        yield return new WaitForSeconds(1f);
        SpawnEnemyAtRandomSpawnner();
        _enemiesAlive++;

    }

    private void SpawnEnemyAtRandomSpawnner()
    {
        //randomly selects one of three spawners
        int randomSpawner = Random.Range(1,4); //returns 1,2 or 3

        switch (randomSpawner)
        {
            case 1:
                SpawnEnemyAtSpawner(enemySpawnner1);
                break;
            case 2:
                SpawnEnemyAtSpawner(enemySpawnner2);
                break;
            case 3:
                SpawnEnemyAtSpawner(enemySpawnner3);
                break;

        }
    }

    private void OnDestroy()
    {
        //unsubscribe from the event when this object is destroyed

        EnemyHealth.OnEnemyDead -= SpawnNewEnemyOnDeath;
        
    }

   
}
