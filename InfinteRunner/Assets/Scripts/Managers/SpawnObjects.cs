using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnObjects : MonoBehaviour
{
    [SerializeField] private float timeToSpawnMin;
    [SerializeField] private float timeToSpawnMax;
    [SerializeField] private bool canSpawn = true;
    [SerializeField] private PoolingSystem pool;
    [SerializeField] private PoolingSystem poolDown;
    private Coroutine _spawningObj;
    private Coroutine _spawningObjDown;
    private GameObject pooledObjUp;
    private GameObject pooledObjDown;

    [SerializeField] private float timeToSpawnUp;
    [SerializeField] private float timeToSpawnDown;
    private void OnEnable()
    {
        ScoreKeeper.OnScoreThresholdReached += IncreaseSpeed;
    }

    private void OnDisable()
    {
        ScoreKeeper.OnScoreThresholdReached -= IncreaseSpeed;
    }

    private void IncreaseSpeed()
    {
        if (timeToSpawnMax > timeToSpawnMin - 0.5f)
        {
            timeToSpawnMax -= 0.5f;
        }
    }

    private void Start()
    {
        StartSpawning();
        StartSpawningDown();
    }

    private void StartSpawning()
    {
        if (_spawningObj != null) 
        {
            StopCoroutine(_spawningObj);
            _spawningObj = null;
        }
        _spawningObj = StartCoroutine(SpawnObj());
    }

    private void StartSpawningDown()
    {
        if (_spawningObjDown != null) 
        {
            StopCoroutine(_spawningObjDown);
            _spawningObjDown = null;
        }
        _spawningObjDown = StartCoroutine(SpawnObjDown());
    }
    private IEnumerator SpawnObj()
    {
        while (canSpawn)
        {
            pooledObjUp = pool.AskForObject();
            timeToSpawnUp = Random.Range(timeToSpawnMin, timeToSpawnMax);
            yield return new WaitForSeconds(timeToSpawnUp);
        }
    }
    private IEnumerator SpawnObjDown()
    {
        while (canSpawn)
        {
            pooledObjDown = poolDown.AskForObject();
            timeToSpawnDown = Random.Range(timeToSpawnMin, timeToSpawnMax);
            yield return new WaitForSeconds(timeToSpawnDown);
        }
    }
}