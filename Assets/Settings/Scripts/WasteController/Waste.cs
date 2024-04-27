using UnityEngine;

[CreateAssetMenu(fileName = "New Waste", menuName = "TurboTrash/Waste")]
public class Waste : ScriptableObject
{
    public enum WasteType { Organic, Plastic, Glass, Paper, Metal }
    public WasteType wasteType;
}