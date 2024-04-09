using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Mission", menuName = "Create New Mission")]
public class DM : ScriptableObject
{
    public string MissionName;
    public string MissionText;
    public List<MissionGoals> Goals = new List<MissionGoals>();
    //public Reward CompletionReward;

    public bool IsComplete
    {
        get
        {
            foreach(MissionGoals goal in Goals)
            {
                if (!goal.IsComplete)
                {
                    return false;
                }
            }
            return true;
        }
    }

    public string GetObjectiveText()
    {
        string objectiveText = "";

        foreach(MissionGoals objective in Goals)
        {
            //objectiveText += objective.GetObjectiveText();
            objectiveText += "\n";
        }

        return objectiveText;
    }


    public void Reset()
    {
        foreach(MissionGoals o in Goals)
        {
            o.Reset();
        }
    }
}