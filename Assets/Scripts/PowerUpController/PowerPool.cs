using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPool : MonoBehaviour
{
    public static PowerPool Instance { get; private set; }
    [SerializeField] private GameObject slowPrefab;
    [SerializeField] private GameObject multiPrefab;
    [SerializeField] private GameObject gravityPrefab;
    [SerializeField] private int PowerPoolSize = 3;
    private Queue<GameObject> powerPool = new Queue<GameObject>();

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
        GameObject powerUpContainer = new GameObject("Power Ups");

        for (int i = 0; i < PowerPoolSize; i++)
        {
            var newpower2 = Instantiate(multiPrefab);
            newpower2.transform.SetParent(powerUpContainer.transform);
            newpower2.SetActive(false);
            powerPool.Enqueue(newpower2);
        }
        for (int i = 0; i < PowerPoolSize; i++)
        {
            var newpower = Instantiate(gravityPrefab);
            newpower.transform.SetParent(powerUpContainer.transform);
            newpower.SetActive(false);
            powerPool.Enqueue(newpower);
        }
        for (int i = 0; i < PowerPoolSize; i++)
        {
            var newpower = Instantiate(slowPrefab);
            newpower.transform.SetParent(powerUpContainer.transform);
            newpower.SetActive(false);
            powerPool.Enqueue(newpower);
        }
    }

    public GameObject GetPower()
    {
        if (powerPool.Count == 0)
        {
            Debug.LogWarning("error empty.");
            RegeneratePower();
        }

        var power = powerPool.Dequeue();
        power.SetActive(true);
        return power;
    }

    void RegeneratePower() 
    {
        for (int i = 0; i < PowerPoolSize; i++)
        {
                int regenerate = Random.Range(1, 4);
                if (regenerate == 1)
                {
                    var newpower = Instantiate(multiPrefab);
                    newpower.SetActive(false);
                    powerPool.Enqueue(newpower);
                }
                if (regenerate == 2)
                {
                    var newpower = Instantiate(slowPrefab);
                    newpower.SetActive(false);
                    powerPool.Enqueue(newpower);
                }
                if (regenerate == 3)
                {
                    var newpower = Instantiate(gravityPrefab);
                    newpower.SetActive(false);
                    powerPool.Enqueue(newpower);
                }
            }
    }

    public void ReturnPower(GameObject power)
    {
        power.SetActive(false);
        powerPool.Enqueue(power);
    }
}