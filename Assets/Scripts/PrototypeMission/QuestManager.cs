using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMManager : MonoSingleton<DMManager>
{
    public DailyCollection Missions;
    public DM Dailymission;

    public bool IsComplete()
    {
        for (int i = 0; i < Dailymission.Goals.Count; i++)
        {
            if (!Dailymission.Goals[i].IsComplete)
            {
                return false;
            }
        }
        return true;
    }


    public void AddPlastic50()
    {
        if (Dailymission != null && !Dailymission.IsComplete)
        {
            for (int i = 0; i < Dailymission.Goals.Count; i++)
            {
                if (Dailymission.Goals[i].PlasticCount >=0
                    && Dailymission.Goals[i].target == Waste.WasteType.Plastic)
                {
                    Dailymission.Goals[i].PlasticCount++;
                    Dailymission.Goals[i].CompleteObjective();
                }
            }
        }
    }

    public void AddFirstRow()
    {
        if (Dailymission != null && !Dailymission.IsComplete)
        {
            for (int i = 0; i < Dailymission.Goals.Count; i++)
            {
                //if (Dailymission.Goals[i].BinsFirstRow == false
                //    && Dailymission.Goals[i].target == Waste.WasteType.Plastic)
                //{
                //    Dailymission.Goals[i].PlasticCount++;
                //    Dailymission.Goals[i].CompleteObjective();
                //}
            }
        }
    }

}
