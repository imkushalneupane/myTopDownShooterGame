
using System.Collections;
using TMPro;
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
    private int killCounter = 0;

    [SerializeField]
    EnemyHealth _enemyHealth;  //refrence to EnemyHealth Script

    [SerializeField]
    private TextMeshProUGUI _killCount;


    public void playerEnters()
    {

        startspawning();
    }

    public void startspawning()
    {
        _enemyHealth.OnEmenyDead2 += SpawnNewEnemyOnDeath; //event subscription
        _enemyHealth.OnEmenyDead2 += IncreaseKillCounter;
        SpawnInitialEnemies();

    }

    private void IncreaseKillCounter()
    {
        killCounter++;
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
        if (killCounter < 10)  //spawns until kill count 10 
        { 
        GameObject newEnemy = Instantiate(_EnemyPrefab, spawner.position, Quaternion.identity);
        Debug.Log($"Spawned enemy at {spawner.name}");

        _killCount.text = killCounter.ToString();
        }  
        else if (killCounter == 10)  //when kill count 10 , spawn AK
        {
            spawnGun();
        }
    }

    private void SpawnNewEnemyOnDeath()
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

        _enemyHealth.OnEmenyDead2 -= SpawnNewEnemyOnDeath;
        _enemyHealth.OnEmenyDead2 -= IncreaseKillCounter;

    }
    
    
    private void spawnGun()
    {
        GameObject newGun = Instantiate(_ak47, gunSpawnPoint.position, Quaternion.identity);
    }
}
    

   

