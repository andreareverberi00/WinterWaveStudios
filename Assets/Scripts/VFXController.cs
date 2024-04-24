using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum VFXType
{
    Explosion,
    Sparkles,
    Smoke,
    PowerUp
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
    private Dictionary<VFXType, GameObject> activeVFXInstances = new Dictionary<VFXType, GameObject>();

    void Start()
    {
        foreach (var entry in vfxEntries)
        {
            vfxDictionary[entry.type] = entry.prefab;
        }
    }

    public void PlayVFXAtPosition(VFXType type, Vector3 position, float duration)
    {
        if (activeVFXInstances.TryGetValue(type, out GameObject existingVFXInstance))
        {
            if (existingVFXInstance != null)
            {
                Destroy(existingVFXInstance); // Destroy the existing VFX instance
            }
        }

        if (vfxDictionary.TryGetValue(type, out GameObject vfxPrefab))
        {
            GameObject vfxInstance = Instantiate(vfxPrefab, position, Quaternion.identity);
            activeVFXInstances[type] = vfxInstance; // Update the active instance
            StartCoroutine(ManageVFXLifetime(vfxInstance, type, duration));
        }
        else
        {
            Debug.LogWarning("VFX type not found: " + type);
        }
    }

    private IEnumerator ManageVFXLifetime(GameObject vfxInstance, VFXType type, float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(vfxInstance);
        activeVFXInstances[type] = null; // Reset the reference once the VFX is destroyed
    }
}
