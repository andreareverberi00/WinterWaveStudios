using System.Collections;
using UnityEngine;

public class PowePoolController : MonoSingleton<PowePoolController>
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float spawnInterval = 10f;
    //[SerializeField, Range(0, 100)] private int spawnSLowProbability = 10;
    private bool isProductionPaused = false;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }
    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval + Random.Range(-0.25f, 0.25f));
            if (!isProductionPaused) // Controlla se la produzione è in pausa
            {
                int RandomPowerSpawn = Random.Range(0, 101);
                if (RandomPowerSpawn <= 30)
                {
                    WasteController.Instance.PauseProduction(.5f); // Pausa la produzione di rifiuti per x secondi
                    SpawnSlow();
                }
                else if (RandomPowerSpawn <= 60)
                {
                    WasteController.Instance.PauseProduction(.5f);
                    SpawnMulti();
                }
                else if (RandomPowerSpawn <= 100)
                {
                    WasteController.Instance.PauseProduction(.5f);
                    SpawnGravity();
                }
            }
        }
    }
    public void PausePowerProduction(float seconds)
    {
        StartCoroutine(PauseProductionRoutine(seconds));
    }

    private IEnumerator PauseProductionRoutine(float seconds)
    {
        isProductionPaused = true;
        yield return new WaitForSeconds(seconds);
        isProductionPaused = false;
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
