using UnityEngine;

public class ScoreController : MonoSingleton<ScoreController>
{
    public int Score { get; private set; }
    public int Highscore { get; private set; }
    public int Coins { get; private set; }

    public int coinsForEveryXScore = 100; // Ogni 100 punti guadagnerai un certo numero di monete
    public int coinsReward = 10; // Numero di monete guadagnate per ogni soglia raggiunta
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
        Coins = PlayerPrefs.GetInt("Coins", 0);

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
    private void RewardCoins()
    {
        if (Score >= coinsForEveryXScore)
        {
            int multiplier = Score / coinsForEveryXScore; // Quante volte il giocatore ha raggiunto la soglia
            Coins += multiplier * coinsReward;
            PlayerPrefs.SetInt("Coins", Coins); // Salva le monete nel PlayerPrefs
            Score -= coinsForEveryXScore * multiplier; // Resetta i punti che hanno già contribuito alla ricompensa
        }
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
        RewardCoins();

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
