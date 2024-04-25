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
    public int HighestConsecutiveCorrectThrows { get; private set; }  // Record per la sessione corrente

    public int scoreAmount;
    public int half;
    public int Double;
    public int initial;
    //public int numberOfCorrectThrowsForReward = 4;
    public int rewardScore = 10;
    //public int rewardEnergy = 10;
    private int tempScore;
    bool alreadystreak;
       private void Start()
    {
        initial = scoreAmount;
        half = scoreAmount / 2;
        Double = scoreAmount * 2;
        Highscore = PlayerPrefs.GetInt("Highscore", 0);
        Coins = PlayerPrefs.GetInt("Coins", 0);
        alreadystreak = false;
        Score = 0;
        CorrectlyThrownWastes = 0;
        MissedWastes = 0;
        ConsecutiveCorrectThrows = 0;
        HighestConsecutiveCorrectThrows = 0;

    }

    public void AddScore()
    {
        Score += scoreAmount;
        GameController.Instance.AddScore();
        UpdateScoreUI();
    }
    private void RewardCoins()
    {
        if (tempScore >= coinsForEveryXScore)
        {
            tempScore=Score;
            int multiplier = tempScore / coinsForEveryXScore; // Quante volte il giocatore ha raggiunto la soglia
            Coins += multiplier * coinsReward;
            PlayerPrefs.SetInt("Coins", Coins); // Salva le monete nel PlayerPrefs
            tempScore -= coinsForEveryXScore * multiplier; // Resetta i punti che hanno già contribuito alla ricompensa
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
        Score -= scoreAmount;
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
         if(ConsecutiveCorrectThrows % 5 == 0  && ConsecutiveCorrectThrows!=0)
        {

            int compliments;
            compliments = Random.Range(1, 6);
            Compliment(compliments);
            alreadystreak = true;
        }
        if (ConsecutiveCorrectThrows > HighestConsecutiveCorrectThrows)
        {
            HighestConsecutiveCorrectThrows = ConsecutiveCorrectThrows;
           
            AddScore();
        }
        UIController.Instance.ShowMaxStreak(HighestConsecutiveCorrectThrows);
    }

    public void RecordMissedThrow()
    {
        MissedWastes++;
        ConsecutiveCorrectThrows = 0;
       if(alreadystreak==true)
        {
            int insults;
            insults = Random.Range(1, 7);
            Insult(insults);
            alreadystreak = false;

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
    void Insult(int I)
    {
        switch (I)
        {
            case 1:
                SpeakerSpeakController.Instance.PlaySound("insult 1");
                break;
            case 2:
                SpeakerSpeakController.Instance.PlaySound("insult 2");
                break;
            case 3:
                SpeakerSpeakController.Instance.PlaySound("insult 3");
                break;
            case 4:
                SpeakerSpeakController.Instance.PlaySound("insult 4");
                break;
            case 5:
                SpeakerSpeakController.Instance.PlaySound("insult 5");
                break;
            case 6:
                SpeakerSpeakController.Instance.PlaySound("insult 6");
                break;
                //default:
                //    break;
        }
    }
    void Compliment(int I)
    {
        switch (I)
        {
            case 1:
                SpeakerSpeakController.Instance.PlaySound("compliment 1");
                break;
            case 2:
                SpeakerSpeakController.Instance.PlaySound("compliment 2");
                break;
            case 3:
                SpeakerSpeakController.Instance.PlaySound("compliment 3");
                break;
            case 4:
                SpeakerSpeakController.Instance.PlaySound("compliment 4");
                break;
            case 5:
                SpeakerSpeakController.Instance.PlaySound("compliment 5");
                break;

                //default:
                //    break;
        }
    }
  
}
