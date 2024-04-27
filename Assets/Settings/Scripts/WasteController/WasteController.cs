using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteController : MonoSingleton<WasteController>
{
    [SerializeField] private List<BinDataHolder> activeBins = new List<BinDataHolder>();
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float initialSpawnInterval = 2.5f;
    [SerializeField] private float minimumSpawnInterval = 1.5f; // Il minimo intervallo di spawn raggiungibile
    [SerializeField] private float spawnDecayRate = 0.05f; // La quantità di tempo che si sottrae dall'intervallo di spawn
    [SerializeField, Range(0, 100)] private int spawnBatteryProbability = 10;

    [Header("Debug")]
    [SerializeField] private float spawnInterval;

    void Start()
    {
        spawnInterval = initialSpawnInterval;
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            bool spawnBattery = Random.Range(0, 100) < spawnBatteryProbability;
            if (spawnBattery)
            {
                SpawnBattery();
            }
            else
            {
                SpawnWaste();
            }

            UpdateSpawnInterval();
        }
    }

    void UpdateSpawnInterval()
    {
        if (spawnInterval > minimumSpawnInterval)
        {
            spawnInterval -= spawnDecayRate;
            spawnInterval = Mathf.Max(spawnInterval, minimumSpawnInterval);
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
        GameObject waste = WastePool.Instance.GetWasteMatchingBins(activeBins);
        if (waste != null)
        {
            waste.transform.position = spawnPosition.position;
        }
        else
        {
            Debug.LogWarning("No matching waste available to spawn.");
        }
    }

    public void AddBin(BinDataHolder newBin)
    {
        if (!activeBins.Contains(newBin))
        {
            activeBins.Add(newBin);
        }
    }
    public void ActivateBin(BinDataHolder binDataHolder)
    {
        if (!activeBins.Contains(binDataHolder))
        {
            activeBins.Add(binDataHolder);
        }
    }
    public Vector3? GetBinPositionForWasteType(WasteDataHolder type)
    {
        foreach (BinDataHolder bin in activeBins)
        {
            if (bin.binData.acceptsType == type.wasteData.wasteType)
            {
                Debug.Log("Bin: " + bin.binData + " Type: " + type.wasteData);

                return bin.transform.position;
            }
        }
        Debug.LogWarning("No bin found for waste type: " + type);
        return null; // Nessun bin trovato per questo tipo di rifiuto
    }

    public void RemoveBin(BinDataHolder binToRemove)
    {
        if (activeBins.Contains(binToRemove))
        {
            activeBins.Remove(binToRemove);
        }
    }
}
