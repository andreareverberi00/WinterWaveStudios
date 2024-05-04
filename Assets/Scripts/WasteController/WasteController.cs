using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteController : MonoSingleton<WasteController>
{
    public List<BinDataHolder> activeBins = new List<BinDataHolder>();

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
        return null;
    }

    public void RemoveBin(BinDataHolder binToRemove)
    {
        if (activeBins.Contains(binToRemove))
        {
            activeBins.Remove(binToRemove);
        }
    }
}
