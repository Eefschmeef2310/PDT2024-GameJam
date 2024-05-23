using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        [Tooltip("Name of the wave")]
        public string waveName;

        [Tooltip("Array of different enemy prefabs")]
        public GameObject[] enemyTypes;

        [Tooltip("Number of enemies per group spawn")]
        public int enemiesPerGroup; 

        [Tooltip("Time in seconds between each group spawn")]
        public float enemySpawnFrequency;

        [Tooltip("Duration of the wave in seconds")]
        public float waveDuration;

        [Tooltip("if a boss should spawn")]
        public bool spawnBoss;

        [Tooltip("Boss prefab")]
        public GameObject bossPrefab;

        [Tooltip("Time in seconds between boss spawns")]
        public float bossSpawnFrequency;
    }

    [Tooltip("Add new wave here")]
    public Wave[] waves;

    [Tooltip("control spawn points here")]
    public Transform[] spawnPoints;
    
    [Tooltip("index / spawn order of wave")]
    private int currentWaveIndex = 0;
    
    [Tooltip("check true if you want wave spawning")]
    private bool spawningWave = false;

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        while (currentWaveIndex < waves.Length)
        {
            if (!spawningWave)
            {
                Wave currentWave = waves[currentWaveIndex];
                spawningWave = true;
                yield return StartCoroutine(HandleWave(currentWave));
                spawningWave = false;
                currentWaveIndex++;
                yield return new WaitForSeconds(10); // Short delay between waves
            }
        }
    }

    private IEnumerator HandleWave(Wave wave)
    {
        float elapsedTime = 0f;
        while (elapsedTime < wave.waveDuration)
        {
            // Spawn enemy groups
            StartCoroutine(SpawnEnemyGroup(wave));

            // Spawn boss if applicable
            if (wave.spawnBoss)
            {
                StartCoroutine(SpawnBoss(wave.bossPrefab, wave.bossSpawnFrequency));
            }

            elapsedTime += wave.enemySpawnFrequency;
            yield return new WaitForSeconds(wave.enemySpawnFrequency); // Wait for the next group spawn
        }
    }

    private IEnumerator SpawnEnemyGroup(Wave wave)
    {
        for (int i = 0; i < wave.enemiesPerGroup; i++)
        {
            SpawnEnemy(wave.enemyTypes[Random.Range(0, wave.enemyTypes.Length)]);
        }
        yield return null;
    }

    private IEnumerator SpawnBoss(GameObject boss, float spawnFrequency)
    {
        while (true)
        {
            SpawnEnemy(boss);
            yield return new WaitForSeconds(spawnFrequency);
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
