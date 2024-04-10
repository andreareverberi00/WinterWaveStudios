using UnityEngine;
using System.Collections.Generic;
using System.Text;

[System.Serializable]
public class MissionGoals
{
    //  throw 50 glass items in correct bin,
//    play a full run without plastic items,
//use only bins in the first row,
    public int PlasticCount;
    public Waste.WasteType target ;

    // explore quests
    public bool RunwithoutPlastic;
    public bool BinsFirstRow;
    [HideInInspector]
    //public List<Room> ExploredRooms = new List<Room>();

    bool isComplete;
    public bool IsComplete { get { return isComplete; } }

    int ObjectiveCount;

    public bool CompleteObjective()
    {
        ObjectiveCount++;

        //  throw 50 glass items in correct bin,
        if (PlasticCount >= 50 && ObjectiveCount >= PlasticCount)
        {
            isComplete = true;
        }

        //    play a full run without plastic items,
        if (RunwithoutPlastic == true /*&& ObjectiveCount >= TargetExploreRoomsCount*/)
        {
            isComplete = true;
        }
        //use only bins in the first row,
        if (BinsFirstRow/* && ObjectiveCount >= TargetExploreRoomsCount*/)
        {
            isComplete = true;
        }
        //UIController.Instance.PlayerHUD.UpdateQuestText();

        return false;
    }

    public void Reset()
    {
        ObjectiveCount = 0;
        //ExploredRooms.Clear();
        isComplete = false;
    }

    //public string GetObjectiveText()
    //{
    //    //StringBuilder sb = new StringBuilder();

    //    //if (TargetKillCount > 0)
    //    //{
    //    //    //sb.Append("Kill " + TargetType.ToString() + " : ");
    //    //    if (IsComplete)
    //    //        sb.AppendLine("Complete!");
    //    //    else
    //    //        sb.AppendLine(ObjectiveCount + " / " + TargetKillCount);
    //    //}

    //    //if (TargetExploreRoomsCount > 0)
    //    //{
    //    //    sb.Append("Explore Rooms : ");
    //    //    if (IsComplete)
    //    //        sb.AppendLine("Complete!");
    //    //    //else
    //    //        //sb.AppendLine(ExploredRooms.Count + " / " + TargetExploreRoomsCount);
    //    //}

    //    //return sb.ToString();
    //}
}