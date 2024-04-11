using UnityEngine;

public class ScoreController : MonoSingleton<ScoreController>
{
    public int Score { get; private set; }
    public int Highscore { get; private set; }

    public int OvertimePeriods { get; private set; }

    public int CorrectlyThrownWastes { get; private set; }
    public int MissedWastes { get; private set; }
    public int ConsecutiveCorrectThrows { get; private set; }

    public int amount;
    public int numberOfCorrectThrowsForReward = 4;
    public int rewardScore = 50;
    public int rewardEnergy = 10;

    private void Start()
    {
        Highscore = PlayerPrefs.GetInt("Highscore", 0);

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

    private void UpdateHighscore()
    {
        if (Score > Highscore)
        {
            Highscore = Score;
            PlayerPrefs.SetInt("Highscore", Highscore);
        }
    }

    public void AddOvertimePeriod()
    {
        OvertimePeriods++;
        UpdateOvertimePeriodsUI();
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

            ConsecutiveCorrectThrows = 0;
            UpdateScoreUI();

            UIController.Instance.ShowStreakFeedback(numberOfCorrectThrowsForReward);
        }
    }

    private void UpdateScoreUI()
    {
        UIController.Instance.SetScore(Score);
    }

    private void UpdateOvertimePeriodsUI()
    {
        UIController.Instance.SetOvertimePeriods(OvertimePeriods);
    }

    public string CalculateGrade()
    {
        UpdateHighscore();

        if (CorrectlyThrownWastes + MissedWastes == 0)
            return "N/A"; // No throws were made

        float correctPercentage = (float)CorrectlyThrownWastes / (CorrectlyThrownWastes + MissedWastes) * 100;

        if (correctPercentage >= 98)
        {
            return "S";
        }
        else if (correctPercentage >= 90)
        {
            return "A";
        }
        else if (correctPercentage >= 80)
        {
            return "B";
        }
        else if (correctPercentage >= 70)
        {
            return "C";
        }
        else if (correctPercentage >= 60)
        {
            return "D";
        }
        else
        {
            return "F";
        }
    }
}
