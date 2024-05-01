using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Diagnostics;

public class LeaderboardScoreManager : MonoBehaviour
{
    // [SerializeField]
    // private TextMeshProUGUI inputScore;
    [SerializeField]
    private TMP_InputField inputName;

    public UnityEvent<string, int> submitScoreEvent;

    public void SubmitScore(int finalScore)
    {   
        string playerName = "Player"+Random.Range(100,1000);
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            playerName = PlayerPrefs.GetString("PlayerName");
        }

        submitScoreEvent.Invoke(playerName, finalScore);
    }
}
