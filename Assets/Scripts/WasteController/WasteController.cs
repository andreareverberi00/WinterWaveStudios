using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteController : MonoBehaviour
{
    //[SerializeField] private List<Bin> activeBins = new List<Bin>();
    [SerializeField] private Transform spawnPosition;
    //[SerializeField] private float initialSpawnDelay = 2f;
    [SerializeField] private float spawnInterval = 3f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnWaste();
            //AdjustSpawnTiming();
        }
    }

    private void SpawnWaste()
    {
        GameObject waste = WastePool.Instance.GetWaste();
        waste.transform.position = spawnPosition.position;
    }

    /*private void AdjustSpawnTiming()
    {
        spawnInterval = Mathf.Max(spawnInterval * 0.95f, 1f);
    }*/

    public bool CheckWasteAndBin(GameObject wasteSelected, GameObject binSelected)
    {
        WasteDataHolder wasteHolder = wasteSelected.GetComponent<WasteDataHolder>();
        BinDataHolder binHolder = binSelected.GetComponent<BinDataHolder>();

        if (wasteHolder != null && binHolder != null && wasteHolder.wasteData.wasteType == binHolder.binData.acceptsType)
        {
            Debug.Log("Correct sorting!");
            WastePool.Instance.ReturnWaste(wasteSelected);
            return true;
        }
        else
        {
            Debug.Log("Incorrect sorting.");
            WastePool.Instance.ReturnWaste(wasteSelected);
            return false;
        }
    }

}
