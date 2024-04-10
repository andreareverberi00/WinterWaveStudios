using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteController : MonoBehaviour
{
    [SerializeField] private List<BinDataHolder> activeBins = new List<BinDataHolder>();
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField, Range(0, 100)] private int spawnBatteryProbability = 10;

    public static WasteController Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval + Random.Range(-0.25f, 0.25f));

            bool spawnBattery = Random.Range(0, 100) < spawnBatteryProbability;

            if (spawnBattery)
            {
                SpawnBattery();
            }
            else
            {
                SpawnWaste();
            }
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

    public void RemoveBin(BinDataHolder binToRemove)
    {
        if (activeBins.Contains(binToRemove))
        {
            activeBins.Remove(binToRemove);
        }
    }
}
