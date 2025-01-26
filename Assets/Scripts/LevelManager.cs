using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float moduleUnitOffset = 4.0f;
    [SerializeField] private float spawnTimer = 5.0f;
    [SerializeField] private int initialModules = 5;
    [SerializeField] private int minNumEnemy = 1;
    [SerializeField] private int maxNumEnemy = 4;
    [SerializeField] private GameObject[] _modules;
    
    private int _lastSpawnedModuleIndex;
    
    private Vector3 nextModuleLocation;

    private void Start()
    {
        nextModuleLocation = this.transform.position;
        
        for (int i = 0; i < initialModules; ++i)
        {
            SpawnModule();
        }

        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(spawnTimer);
        SpawnModule();
        StartCoroutine(SpawnCoroutine());

    }

    private void SpawnModule()
    {
        int randomIndex = Random.Range(0, _modules.Length);
        while (randomIndex == _lastSpawnedModuleIndex)
        {
            randomIndex = Random.Range(0, _modules.Length);
        }

        GameObject selectedModule = _modules[randomIndex];
        _lastSpawnedModuleIndex = randomIndex;
        GameObject newModule = Instantiate(selectedModule, nextModuleLocation, Quaternion.identity);

        int randomNumEnemy = Random.Range(minNumEnemy, maxNumEnemy);
        
        EnemySpawnPoint[] EnemySpawnPoints = newModule.GetComponentsInChildren<EnemySpawnPoint>();
        foreach (EnemySpawnPoint spawnPoint in EnemySpawnPoints)
        {
            if (randomNumEnemy == 0)
                break;
            
            float randomValue = Random.value;
            if (randomValue > 0.5f)
            {
                spawnPoint.SpawnEnemy();
                randomNumEnemy--;
            }
        }
        
        IncreaseModuleLocation();
    }

    void IncreaseModuleLocation()
    {
        nextModuleLocation.z += moduleUnitOffset;
    }
}
