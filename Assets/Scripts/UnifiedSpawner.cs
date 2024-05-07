using System.Collections;
using UnityEngine;

public class UnifiedSpawner : MonoSingleton<UnifiedSpawner>
{
    [SerializeField] private Transform spawnPosition;

    [Header("Spawn Intervals")]
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float minSpawnInterval = 1.5f;  // Il minimo intervallo di spawn raggiungibile
    [SerializeField] private float spawnDecayRate = 0.001f; // La quantità di tempo che si sottrae dall'intervallo di spawn ad ogni ciclo
    private float startSpawnInterval;
    private float doubledSpawnInterval;

    [Header("Spawn Probabilities")]
    [Range(0, 100)] public int wasteProbability = 50;
    [Range(0, 100)] public int batteryProbability = 30;
    [Range(0, 100)] public int powerUpProbability = 20;

    [Header("Power-Up Probabilities")]
    [Range(0, 100)] public int slowProbability = 33;
    [Range(0, 100)] public int multiProbability = 33;
    [Range(0, 100)] public int gravityProbability = 34;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
        startSpawnInterval = spawnInterval;
        doubledSpawnInterval = startSpawnInterval * 2;
    }
    private void Update()
    {
        if (GameController.Instance.slow == true)
        {
            spawnInterval = doubledSpawnInterval;
        }
        else
        {
            spawnInterval = startSpawnInterval;
        }
    }
    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            int random = Random.Range(0, 101);
            if (random <= wasteProbability)
            {
                SpawnWaste();
            }
            else if (random <= wasteProbability + batteryProbability)
            {
                SpawnBattery();
            }
            else
            {
                SpawnPowerUp();
            }
            UpdateSpawnInterval();
        }
    }

    void SpawnBattery()
    {
        GameObject battery = WastePool.Instance.GetBattery();
        if (battery != null)
        {
            battery.transform.position = spawnPosition.position;
        }
    }


    void SpawnWaste()
    {
        GameObject waste = WastePool.Instance.GetWasteMatchingBins(WasteController.Instance.activeBins);
        if (waste != null)
        {
            waste.transform.position = spawnPosition.position;
        }
        else
        {
            Debug.LogWarning("No matching waste available to spawn.");
        }
    }
    void UpdateSpawnInterval()
    {
        if (spawnInterval > minSpawnInterval)
        {
            spawnInterval -= spawnDecayRate;
            spawnInterval = Mathf.Max(spawnInterval, minSpawnInterval);
        }
        
    }
    void SpawnPowerUp()
    {
        int powerUpRoll = Random.Range(0, 101);
        if (powerUpRoll <= slowProbability)
        {
            GameObject slow = PowerPool.Instance.GetPower();
            if (slow != null)
            {
                slow.transform.position = spawnPosition.position;
            }
        }
        else if (powerUpRoll <= slowProbability + multiProbability)
        {
            GameObject multi = PowerPool.Instance.GetPower();
            if (multi != null)
            {
                multi.transform.position = spawnPosition.position;
            }
        }
        else
        {
            GameObject gravity = PowerPool.Instance.GetPower();
            if (gravity != null)
            {
                gravity.transform.position = spawnPosition.position;
            }
        }
    }
}
