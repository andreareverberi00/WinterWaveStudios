using System.Collections.Generic;
using UnityEngine;

public class WastePool : MonoBehaviour
{
    public static WastePool Instance { get; private set; }

    [SerializeField] private GameObject wastePrefab;
    [SerializeField] private int poolSize = 20;
    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newWaste = Instantiate(wastePrefab);
            newWaste.SetActive(false);
            pool.Enqueue(newWaste);
        }
    }

    public GameObject GetWaste()
    {
        GameObject waste;
        if (pool.Count > 0)
        {
            waste = pool.Dequeue();
        }
        else
        {
            waste = Instantiate(wastePrefab);
        }

        waste.SetActive(true);
        return waste;
    }

    public void ReturnWaste(GameObject waste)
    {
        waste.SetActive(false);
        pool.Enqueue(waste);
    }
}
