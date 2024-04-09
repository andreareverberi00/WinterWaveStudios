using System.Collections.Generic;
using UnityEngine;
using static Waste;

public class ScoreController : MonoSingleton<ScoreController>
{
    public int Score { get; private set; }
    public int CorrectlyThrownWastes { get; private set; }
    public int MissedWastes { get; private set; }
    public int ConsecutiveCorrectThrows { get; private set; }

    public int amount; 
    public int numberOfCorrectThrowsForReward = 4; 
    public int rewardScore = 10; 
    public int rewardEnergy = 10;

    private void Start()
    {
        Score = 0;
        CorrectlyThrownWastes = 0;
        MissedWastes = 0;
        ConsecutiveCorrectThrows = 0;
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
        ConsecutiveCorrectThrows++;
        CheckForReward();
    }

    public void RecordMissedThrow()
    {
        MissedWastes++;
        ConsecutiveCorrectThrows = 0;
    }

    private void CheckForReward()
    {
        if (ConsecutiveCorrectThrows >= numberOfCorrectThrowsForReward) 
        {
            Score += rewardScore;
            // or BatteryController.Instance.CollectBattery(10);

            ConsecutiveCorrectThrows = 0;
            UpdateScoreUI();

            UIController.Instance.ShowStreakFeedback(numberOfCorrectThrowsForReward);
        }
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
