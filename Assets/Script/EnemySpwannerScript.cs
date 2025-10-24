
using System.Collections;
using UnityEngine;

public class EnemySpwannerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _EnemyPrefab;
    public GameObject _ak47;

    public Transform gunSpawnPoint;

    public Transform enemySpawnner1;
    public Transform enemySpawnner2;
    public Transform enemySpawnner3;

    private int _enemiesAlive = 0;
    private int counter = 0;


    public void playerEnters()
    {

        startspawning();
    }

    public void startspawning()
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
        counter += 3;
    }

    private void SpawnEnemyAtSpawner(Transform spawner)
    {
        if (counter >= 10) { spawnGun(); return; }
        GameObject newEnemy = Instantiate(_EnemyPrefab, spawner.position, Quaternion.identity);
        Debug.Log($"Spawned enemy at {spawner.name}");
        
       


    }

    private void SpawnNewEnemyOnDeath(Transform deathPosition)
    {
        counter++;

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
        int randomSpawner = Random.Range(1, 4); //returns 1,2 or 3

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
    
    
    private void spawnGun()
    {
        GameObject newGun = Instantiate(_ak47, gunSpawnPoint.position, Quaternion.identity);
    }
}
    

   

