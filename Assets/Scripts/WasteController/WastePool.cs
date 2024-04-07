using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Waste;

public class WastePool : MonoBehaviour
{
    public static WastePool Instance { get; private set; }

    [SerializeField] private List<GameObject> wastePrefabs;
    [SerializeField] private GameObject batteryPrefab;
    [SerializeField] private int poolSize = 20;
    [SerializeField] private int batteryPoolSize = 5;

    // Pool separati per i rifiuti e le batterie
    private Dictionary<WasteType, Queue<GameObject>> wastePools = new Dictionary<WasteType, Queue<GameObject>>();
    private Queue<GameObject> batteryPool = new Queue<GameObject>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        InitializePools();
    }

    void InitializePools()
    {
        // Inizializza i pool per i tipi di rifiuti
        foreach (GameObject prefab in wastePrefabs)
        {
            WasteDataHolder dataHolder = prefab.GetComponent<WasteDataHolder>();
            if (dataHolder == null) continue;

            WasteType wasteType = dataHolder.wasteData.wasteType;
            if (!wastePools.ContainsKey(wasteType))
            {
                wastePools[wasteType] = new Queue<GameObject>();
            }

            for (int i = 0; i < poolSize; i++)
            {
                var newWaste = Instantiate(prefab);
                newWaste.SetActive(false);
                wastePools[wasteType].Enqueue(newWaste);
            }
        }

        // Inizializza il pool per le batterie
        for (int i = 0; i < batteryPoolSize; i++)
        {
            var newBattery = Instantiate(batteryPrefab);
            newBattery.SetActive(false);
            batteryPool.Enqueue(newBattery);
        }
    }

    public GameObject GetWasteMatchingBins(List<BinDataHolder> activeBins)
    {
        List<GameObject> matchingWastes = new List<GameObject>();

        // Raccoglie tutti i rifiuti compatibili
        foreach (var bin in activeBins)
        {
            WasteType type = bin.binData.acceptsType;
            if (wastePools.ContainsKey(type))
            {
                foreach (var waste in wastePools[type])
                {
                    if (!matchingWastes.Contains(waste) && waste.activeInHierarchy == false)
                    {
                        matchingWastes.Add(waste);
                    }
                }
            }
        }

        // Seleziona casualmente da questi rifiuti compatibili
        if (matchingWastes.Count > 0)
        {
            int randomIndex = Random.Range(0, matchingWastes.Count);
            GameObject selectedWaste = matchingWastes[randomIndex];
            selectedWaste.SetActive(true);
            return selectedWaste;
        }
        else
        {
            Debug.LogWarning("No matching waste found for active bins.");
            return null;
        }
    }


    public GameObject GetBattery()
    {
        if (batteryPool.Count == 0)
        {
            Debug.LogError("Battery pool is empty.");
            return null;
        }

        var battery = batteryPool.Dequeue();
        battery.SetActive(true);
        return battery;
    }

    public void ReturnWaste(GameObject waste)
    {
        WasteDataHolder dataHolder = waste.GetComponent<WasteDataHolder>();
        if (dataHolder != null && wastePools.ContainsKey(dataHolder.wasteData.wasteType))
        {
            waste.SetActive(false);
            wastePools[dataHolder.wasteData.wasteType].Enqueue(waste);
        }
        else
        {
            Debug.LogError("Returning waste to pool failed. WasteDataHolder missing or waste type not recognized.");
        }
    }

    public void ReturnBattery(GameObject battery)
    {
        battery.SetActive(false);
        batteryPool.Enqueue(battery);
    }
}
