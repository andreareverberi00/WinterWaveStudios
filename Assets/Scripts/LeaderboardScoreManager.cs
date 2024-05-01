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
        print(inputName.text + " " + finalScore.ToString());
        submitScoreEvent.Invoke(inputName.text, finalScore);
    }
}
