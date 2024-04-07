using System.Collections.Generic;
using UnityEngine;
using static Waste;

public class ScoreController : MonoSingleton<ScoreController>
{
    public int Score { get; private set; }
    public int CorrectlyThrownWastes { get; private set; }
    public int MissedWastes { get; private set; }

    public int amount;
    private void Start()
    {
        Score = 0; 
        CorrectlyThrownWastes = 0;
        MissedWastes = 0;
    }

    public void AddScore()
    {
        Score += amount;
        UpdateScoreUI();
    }
    public void RemoveScore()
    {
        Score -= amount;
        if (Score < 0)
        {
            Score = 0;
        }
        UpdateScoreUI();
    }
    public void RecordCorrectThrow()
    {
        CorrectlyThrownWastes++;
    }

    public void RecordMissedThrow()
    {
        MissedWastes++;
    }
    private void UpdateScoreUI()
    {
        UIController.Instance.SetScore(Score);
        Debug.Log("Score: " + Score);
    }
    public string CalculateGrade()
    {
        if (CorrectlyThrownWastes + MissedWastes == 0)
            return "N/A"; // No throws were made

        float correctPercentage = (float) CorrectlyThrownWastes/ (CorrectlyThrownWastes + MissedWastes) * 100;

        if (correctPercentage >= 90)
        {
            return "A";
        }
        else if (correctPercentage >= 75)
        {
            return "B";
        }
        else if (correctPercentage >= 50)
        {
            return "C";
        }
        else if (correctPercentage >= 25)
        {
            return "D";
        }
        else
        {
            return "F";
        }
    }

}
