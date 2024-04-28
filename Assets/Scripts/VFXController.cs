using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum VFXType
{
    Explosion,
    Sparkles,
    Smoke,
    PowerUp,
    PowerUp2X,
    SpeakerSound,
    CorrectSorting,
    MagnetPowerUp
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

    GameObject vfxContainer;

    void Start()
    {
        foreach (var entry in vfxEntries)
        {
            vfxDictionary[entry.type] = entry.prefab;
        }
        vfxContainer = new GameObject("VFX");
    }

    public void PlayVFXAtPosition(VFXType type, Vector3 position, float duration, bool canHaveMultipleInstances=false)
    {
        if (activeVFXInstances.TryGetValue(type, out GameObject existingVFXInstance)&&!canHaveMultipleInstances)
        {
            if (existingVFXInstance != null)
            {
                Destroy(existingVFXInstance); // Destroy the existing VFX instance
            }
        }

        if (vfxDictionary.TryGetValue(type, out GameObject vfxPrefab))
        {
            GameObject vfxInstance = Instantiate(vfxPrefab, position, Quaternion.identity);
            vfxInstance.transform.SetParent(vfxContainer.transform);
            activeVFXInstances[type] = vfxInstance; // Update the active instance
            StartCoroutine(ManageVFXLifetime(vfxInstance, type, duration));
        }
        else
        {
            Debug.LogWarning("VFX type not found: " + type);
        }
    }
    public void PlayVFXAsChild(VFXType type, Transform parentTransform, Vector3 position, float duration)
    {
        if (vfxDictionary.TryGetValue(type, out GameObject vfxPrefab))
        {
            GameObject vfxInstance = Instantiate(vfxPrefab, position, Quaternion.identity);
            vfxInstance.transform.SetParent(parentTransform);
            StartCoroutine(ManageVFXLifetime(vfxInstance, VFXType.CorrectSorting, duration));
        }
        else
        {
            Debug.LogWarning("VFX type not found: " + VFXType.CorrectSorting);
        }
    }
    private IEnumerator ManageVFXLifetime(GameObject vfxInstance, VFXType type, float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(vfxInstance);
        activeVFXInstances[type] = null; // Reset the reference once the VFX is destroyed
    }
}
