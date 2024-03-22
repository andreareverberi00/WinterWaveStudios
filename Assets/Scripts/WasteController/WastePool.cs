using System.Collections.Generic;
using UnityEngine;

public class WastePool : MonoBehaviour
{
    public static WastePool Instance { get; private set; }

    [SerializeField] private List<GameObject> wastePrefabs; // Lista di diversi prefabs di rifiuti
    [SerializeField] private int poolSize = 20; // Dimensione del pool per ogni tipo di prefab
    private List<Queue<GameObject>> pools = new List<Queue<GameObject>>(); // Lista di code, una per ogni tipo di prefab

    private void Awake()
    {
        Instance = this;
        InitializePools();
    }

    private void InitializePools()
    {
        // Inizializza un pool per ogni tipo di prefab
        foreach (GameObject prefab in wastePrefabs)
        {
            var newPool = new Queue<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                var newWaste = Instantiate(prefab);
                newWaste.SetActive(false);
                newPool.Enqueue(newWaste);
            }
            pools.Add(newPool); // Aggiunge il nuovo pool alla lista di pools
        }
    }

    public GameObject GetWaste()
    {
        // Scegli un pool casuale dalla lista dei pools
        int randomIndex = Random.Range(0, pools.Count);
        var pool = pools[randomIndex];

        // Se la coda scelta è vuota, usa il pool successivo
        if (pool.Count == 0)
        {
            randomIndex = (randomIndex + 1) % pools.Count;
            pool = pools[randomIndex];
        }

        // Prende un GameObject dal pool
        var waste = pool.Dequeue();
        waste.SetActive(true);
        return waste;
    }

    public void ReturnWaste(GameObject waste)
    {
        waste.SetActive(false);
        // Semplicemente rimette il GameObject in uno dei pools senza distinzione
        pools[Random.Range(0, pools.Count)].Enqueue(waste);
    }
}
