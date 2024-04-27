using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Daily Mission Collection", menuName = "Create Daily Mission Collection")]
public class DailyCollection : ScriptableObject
{
    public List<DM> Missions = new List<DM>();

    public DM GetMission()
    {
        return Missions[Random.Range(0, Missions.Count)];
    }
}