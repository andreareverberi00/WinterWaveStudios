using System.Collections.Generic;
using UnityEngine;
public enum VFXType
{
    Explosion,
    Sparkles,
    Smoke,
    Firework
}

public class VFXController : MonoSingleton<VFXController>
{
    [System.Serializable]
    public struct VFXEntry
    {
        public VFXType type;
        public GameObject prefab;
    }

    [SerializeField]
    private VFXEntry[] vfxEntries;
    private Dictionary<VFXType, GameObject> vfxDictionary = new Dictionary<VFXType, GameObject>();

    void Start()
    {
        foreach (var entry in vfxEntries)
        {
            vfxDictionary[entry.type] = entry.prefab;
            print(vfxDictionary[entry.type]);
        }
    }

    public void PlayVFXAtPosition(VFXType type, Vector3 position)
    {
        Debug.Log("Playing VFX at position: " + position.ToString() + "of type: "+ type);
        if (vfxDictionary.TryGetValue(type, out GameObject vfxPrefab))
        {
            GameObject vfxInstance = Instantiate(vfxPrefab, position, Quaternion.identity);
            Destroy(vfxInstance, 5f); // Assumi che l'effetto visivo si auto-distrugga dopo 5 secondi
        }
        else
        {
            Debug.LogWarning("VFX type not found: " + type.ToString());
        }
    }
    public void printmessage()
    {
        Debug.Log("Hello");
    }
}
