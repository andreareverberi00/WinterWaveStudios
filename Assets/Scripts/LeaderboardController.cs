using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class LeaderboardController : MonoBehaviour
{
    [SerializeField] 
    private List<TMP_Text> names;
    [SerializeField] 
    private List<TMP_Text> scores;

    private string publicLeaderboardKey = "5e3cd2b5ed3928e4efa6a4d4cc2467f55fda3d9e553344ff251313e80f995f2f";


    private void Start()
    {
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) => { 
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            print(loopLength);
            for (int i = 0; i < loopLength; ++i)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboardEntry(string Username, int Score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, Username, Score, ((msg) =>
        {
            // if contiene brutte parole return
            GetLeaderboard();
        }));
        LeaderboardCreator.ResetPlayer();
    }
}
