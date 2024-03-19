using UnityEngine;

[CreateAssetMenu(fileName = "New Bin", menuName = "TurboTrash/Bin")]
public class Bin : ScriptableObject
{
    public Waste.WasteType acceptsType;
}