using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemies")] 
    public GameObject[] _enemies;


    [Header("Scene Specific")]
    public GameObject[] _spawnPoints;

    [Header("Configurable")] 
    public int MaximumEnemiesAllowed = 16;
    public float MaximumEnemiesPerSecond = 3;
    public float StartingSpawnRate = .25f;
    public int MaximumPickups = 8;

    [Header("Pickups")]
    public GameObject Pickup;

    public bool SpawnPickups;


    
    public static float SpawnRate = 0;
    public static SpawnManager i;

    public int EnemyCount
    {
        get { return _aliveEnemies; }
        set { _aliveEnemies = value; }
    }

    public int PickupCount
    {
        get { return _activePickups; }
        set { _activePickups = value; }
    }

    private int _aliveEnemies;
    private Vector3 _pickupSpawnOffset;
    private int _activePickups;

    private void Awake()
    {
        if (i == null)
        {
            i = this;
        }
        else
        {
            Destroy(this);
        }

        SpawnRate = StartingSpawnRate;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_spawnPoints == null) return;
        StartCoroutine(SpawningEnemies());
        StartCoroutine(SpawningPickups());
    }

    // Update is called once per frame
    void Update()
    {
        SpawnRate = Mathf.Min(MaximumEnemiesPerSecond, SpawnRate += 0.01f * Time.deltaTime);
        
        _pickupSpawnOffset = new Vector3(Random.Range(-20,20),Random.Range(-20,20),0);
    }

    IEnumerator SpawningEnemies()
    {
        while (true)
        {
            if (_aliveEnemies < MaximumEnemiesAllowed && _spawnPoints.Length > 0)
            {
                SpawnEnemy(_spawnPoints[Random.Range(0, _spawnPoints.Length)].transform, _enemies[Random.Range(0,_enemies.Length)]);
            }
            yield return new WaitForSeconds(1/SpawnRate);
        }
    }

    IEnumerator SpawningPickups()
    {
        while (true)
        {
            if (_activePickups < MaximumPickups && SpawnPickups)
            {
                SpawnPickup();
            }
            yield return new WaitForSeconds(7);
        }
    }

    void SpawnEnemy(Transform positionToSpawn, GameObject enemy)
    {
        var enemyRef = Instantiate(enemy, positionToSpawn.position, positionToSpawn.rotation);
        Stats enemyStats = enemyRef.GetComponent<Stats>();
        Health enemyHealth = enemyRef.GetComponent<Health>();
        enemyHealth.GetHealth *= LevelManager.DifficultyFactor * .5f;
        enemyHealth.GetMaxHealth *= LevelManager.DifficultyFactor * .5f;
        enemyStats.Damage = Mathf.Min(25, enemyStats.Damage *= LevelManager.DifficultyFactor * .5f);
        enemyStats.AttackSpeed = Mathf.Min(2, enemyStats.AttackSpeed *= LevelManager.DifficultyFactor * .5f);
        enemyStats.BulletSpeed = Mathf.Min(16, enemyStats.BulletSpeed *= LevelManager.DifficultyFactor * .5f);
        
        
        _aliveEnemies++;
    }

    void SpawnPickup()
    {
        Instantiate(Pickup, _pickupSpawnOffset, Quaternion.identity);
        _activePickups++;
    }
}
