using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class DMManager : MonoSingleton<DMManager>
//{
//    public QuestCollection Quests;
//    public DM CurrentQuest;

//    public bool IsComplete()
//    {
//        for (int i = 0; i < CurrentQuest.Goals.Count; i++)
//        {
//            if (!CurrentQuest.Goals[i].IsComplete)
//            {
//                return false;
//            }
//        }
//        return true;
//    }


//    public void AddQuestKill(Monster _killed)
//    {
//        if (CurrentQuest != null && !CurrentQuest.IsComplete)
//        {
//            for(int i = 0; i < CurrentQuest.Goals.Count; i++)
//            {
//                if (CurrentQuest.Goals[i].TargetKillCount > 0 
//                    && CurrentQuest.Goals[i].TargetType == _killed.MonsterType)
//                {
//                    CurrentQuest.Goals[i].CompleteObjective();
//                }
//            }
//        }
//    }


//    public void AddExploreRoom(Room _room)
//    {
//        if (CurrentQuest != null && !CurrentQuest.IsComplete)
//        {
//            for (int i = 0; i < CurrentQuest.Goals.Count; i++)
//            {
//                if (CurrentQuest.Goals[i].TargetExploreRoomsCount > 0
//                    && !CurrentQuest.Goals[i].ExploredRooms.Contains(_room))
//                {
//                    if (!CurrentQuest.Goals[i].IsComplete)
//                    {
//                        CurrentQuest.Goals[i].ExploredRooms.Add(_room);
//                        CurrentQuest.Goals[i].CompleteObjective();
//                    }
//                }
//            }
//        }
//    }
//}
