using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WastePool : MonoBehaviour
{
    public static WastePool Instance { get; private set; }

    [SerializeField] private List<GameObject> wastePrefabs;
    [SerializeField] private GameObject batteryPrefab; 
    [SerializeField] private int poolSize = 20; 
    [SerializeField] private int batteryPoolSize = 5; 
    private List<GameObject> pool = new List<GameObject>(); 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        InitializePool();
    }

    private void InitializePool()
    {
        foreach (GameObject prefab in wastePrefabs)
        {
            for (int i = 0; i < poolSize; i++)
            {
                var newWaste = Instantiate(prefab);
                newWaste.SetActive(false);
                pool.Add(newWaste);
            }
        }

        // BatteryPool
        for (int i = 0; i < batteryPoolSize; i++)
        {
            var newBattery = Instantiate(batteryPrefab);
            newBattery.SetActive(false);
            pool.Add(newBattery);
        }
    }

    public GameObject GetWaste()
    {
        if (pool.Count == 0)
        {
            Debug.LogError("Pool is empty");
            return null;
        }

        int randomIndex = Random.Range(0, pool.Count);
        GameObject selectedWaste = pool[randomIndex];
        pool.RemoveAt(randomIndex);
        selectedWaste.SetActive(true);
        return selectedWaste;
    }

    public void ReturnWaste(GameObject waste)
    {
        waste.SetActive(false);
        pool.Add(waste);
    }
}
