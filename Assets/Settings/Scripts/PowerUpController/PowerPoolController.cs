using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowePoolController : MonoSingleton<PowePoolController>
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float spawnInterval = 10f;
    //[SerializeField, Range(0, 100)] private int spawnSLowProbability = 10;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }
    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval + Random.Range(-0.25f, 0.25f));
            int RandomPowerSpawn = Random.Range(0,101);
            if(RandomPowerSpawn >=0 && RandomPowerSpawn <= 30)
            {
                SpawnSlow();
            }
            if (RandomPowerSpawn >= 31 && RandomPowerSpawn <= 60)
            {
                SpawnMulti();
            }
            if (RandomPowerSpawn >= 61 && RandomPowerSpawn <= 100  )
            {
                SpawnGravity();
            }
        }
    }
    void SpawnSlow()
    {
        GameObject slow = PowerPool.Instance.GetPower();
        if (slow != null)
        {
            slow.transform.position = spawnPosition.position;
        }
    }
    void SpawnMulti()
    {
        GameObject multi = PowerPool.Instance.GetPower();
        if (multi != null)
        {
            multi.transform.position = spawnPosition.position;
        }
    }
    void SpawnGravity()
    {
        GameObject gravity = PowerPool.Instance.GetPower();
        if (gravity != null)
        {
            gravity.transform.position = spawnPosition.position;
        }
    }
}
