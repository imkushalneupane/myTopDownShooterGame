using System.Collections;
using TMPro;
using UnityEngine;

public class EnemySpwannerScript : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField]
    private GameObject _EnemyPrefab;

    [Header("Gun Settings")]
    public GameObject _ak47;
    public GameObject _key;
    public Transform keySpawnPoint;
    public Transform gunSpawnPoint;

    [Header("Spawn Points")]
    public Transform enemySpawnner1;
    public Transform enemySpawnner2;
    public Transform enemySpawnner3;

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI _killCount;

    [Header("Spawning Controls")]
    [SerializeField] private int maxEnemiesAlive = 5; // Maximum enemies allowed at once
    [SerializeField] private float spawnDelay = 2f; // Delay between spawns
    [SerializeField] private int killsToSpawnGun = 10; // Kills needed to spawn AK-47
    [SerializeField] private int killsToSpawnKey = 50;
    [SerializeField] private int killsToStopSpawning = 55; // Kills when enemies stop spawning
    

    private int killCounter = 0;
    private int _enemiesAlive = 0;
    private bool hasSpawnedGun = false;
    private bool hasSpawnedKey = false;
    private bool isSpawningActive = false;
    private Coroutine currentSpawningCoroutine;

    public bool IsPlayerInPit = false;

    // Track if we've already processed this enemy death to prevent double counting
    private System.Collections.Generic.HashSet<EnemyHealth> processedEnemies = new System.Collections.Generic.HashSet<EnemyHealth>();

    public void playerEnters()
    {
        startspawning();
    }

    public void startspawning()
    {
        // Subscribe to the single static event
        EnemyHealth.OnEnemyDead += OnEnemyDeathEventHandler;

        isSpawningActive = true;
        SpawnInitialEnemies();

        // Start controlled spawning coroutine
        if (currentSpawningCoroutine != null)
            StopCoroutine(currentSpawningCoroutine);
        currentSpawningCoroutine = StartCoroutine(ControlledSpawningRoutine());
    }

    // Prevent double counting by tracking processed enemies
    private void OnEnemyDeathEventHandler(EnemyHealth deadEnemy)
    {
        // Check if we've already processed this enemy
        if (processedEnemies.Contains(deadEnemy))
            return;

        // Mark this enemy as processed
        processedEnemies.Add(deadEnemy);

        // Handle the death
        if (IsPlayerInPit)
        {
            IncreaseKillCounter();
            _enemiesAlive--;
            Debug.Log($"Enemy died. Enemies alive: {_enemiesAlive}, Kills: {killCounter}");
        }
       

    }

    private void IncreaseKillCounter()
    {
        killCounter++;
        UpdateKillCounterUI();
        Debug.Log($"Kill count: {killCounter}");

        // Spawn gun when reaching the specified kill count
        if (killCounter >= killsToSpawnGun && !hasSpawnedGun)
        {
            spawnGun();
            hasSpawnedGun = true;
        }
        if(killCounter >= killsToSpawnKey && !hasSpawnedKey)
        {
            spawnKey();
            hasSpawnedKey = true;

        }

        // Stop spawning when we reach the stop spawning kill count
        if (killCounter >= killsToStopSpawning)
        {
            StopAllSpawning();
        }
    }

    private void UpdateKillCounterUI()
    {
        if (_killCount != null)
            _killCount.text = "Kills: " + killCounter.ToString();
    }

    private void SpawnInitialEnemies()
    {
        // Spawn initial enemies based on available spawn points
        Transform[] spawners = { enemySpawnner1, enemySpawnner2, enemySpawnner3 };

        for (int i = 0; i < Mathf.Min(3, maxEnemiesAlive); i++)
        {
            if (spawners[i] != null)
            {
                SpawnEnemyAtSpawner(spawners[i]);
            }
        }
        _enemiesAlive = Mathf.Min(3, maxEnemiesAlive);
        UpdateKillCounterUI();
    }

    // Controlled spawning coroutine
    private IEnumerator ControlledSpawningRoutine()
    {
        while (isSpawningActive && killCounter < killsToStopSpawning)
        {
            yield return new WaitForSeconds(spawnDelay);

            // Only spawn if we're below the max enemy limit and haven't reached stop spawning kill count
            if (_enemiesAlive < maxEnemiesAlive && killCounter < killsToStopSpawning)
            {
                SpawnEnemyAtRandomSpawnner();
                _enemiesAlive++;
                Debug.Log($"Spawned new enemy. Total alive: {_enemiesAlive}");
            }
        }
    }

    private void SpawnEnemyAtSpawner(Transform spawner)
    {
        if (killCounter < killsToStopSpawning && _enemiesAlive < maxEnemiesAlive)
        {
            GameObject newEnemy = Instantiate(_EnemyPrefab, spawner.position, Quaternion.identity);
            Debug.Log($"Spawned enemy at {spawner.name}");
        }
    }

    private void SpawnEnemyAtRandomSpawnner()
    {
        // Randomly selects one of three spawners
        int randomSpawner = Random.Range(1, 4); // Returns 1, 2 or 3

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

    // Method to stop all spawning
    private void StopAllSpawning()
    {
        isSpawningActive = false;
        if (currentSpawningCoroutine != null)
        {
            StopCoroutine(currentSpawningCoroutine);
            currentSpawningCoroutine = null;
        }
        Debug.Log($"Spawning stopped - reached {killCounter} kills (stop at: {killsToStopSpawning})!");
    }

    private void spawnGun()
    {
        if (_ak47 != null && gunSpawnPoint != null)
        {
            Instantiate(_ak47, gunSpawnPoint.position, Quaternion.identity);
            Debug.Log($"AK-47 spawned at {killCounter} kills!");
        }
    }
        private void spawnKey()
    {
        if (_key != null && keySpawnPoint != null)
        {
            Instantiate(_key, keySpawnPoint.position, Quaternion.identity);
            Debug.Log($"key spawned at {killCounter} kills!");
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event
        EnemyHealth.OnEnemyDead -= OnEnemyDeathEventHandler;

        // Stop any running coroutines
        if (currentSpawningCoroutine != null)
            StopCoroutine(currentSpawningCoroutine);
    }
}