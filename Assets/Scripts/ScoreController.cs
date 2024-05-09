using UnityEngine;

public class ScoreController : MonoSingleton<ScoreController>
{
    public int Score { get; private set; }
    public int Highscore { get; private set; }
    public int Coins { get; private set; }

    public int OvertimePeriods { get; private set; }

    public int CorrectlyThrownWastes { get; private set; }
    public int MissedWastes { get; private set; }
    public int ConsecutiveCorrectThrows { get; private set; }
    public int HighestConsecutiveCorrectThrows { get; private set; }  // Record per la sessione corrente
    public int punteggio;
    public int scoreAmount;
    public int half;
    public int Double;
    public int initial;
    //public int numberOfCorrectThrowsForReward = 4;
    public int rewardScore = 10;
    //public int rewardEnergy = 10;
    private int tempScore;

    public LeaderboardScoreManager leaderboardScoreManager;

    bool alreadystreak;
       private void Start()
    {
        punteggio = scoreAmount;
        initial = punteggio;
        half = punteggio / 2;
        Double = punteggio * 2;
        Highscore = PlayerPrefs.GetInt("Highscore", 0);
        Coins = PlayerPrefs.GetInt("Coins", 0);
        alreadystreak = false;
        Score = 0;
        CorrectlyThrownWastes = 0;
        MissedWastes = 0;
        ConsecutiveCorrectThrows = 0;
        HighestConsecutiveCorrectThrows = 0;
        DontDestroyOnLoad(this.gameObject);

    }

    public void AddScore()
    {
        Score += punteggio;
        print("Score: "+Score+"\nPunteggio: "+punteggio);
        GameController.Instance.AddScore();
        UpdateScoreUI(punteggio);
    }
    private void RewardCoins()
    {
        tempScore = Score;
    
            tempScore=Score;
            Coins += tempScore/5;

            PlayerPrefs.SetInt("Coins", Coins); // Salva le monete nel PlayerPrefs
            PlayerPrefs.Save();
    
    }
    private void UpdateHighscore()
    {
        if (Score > Highscore)
        {
            Highscore = Score;
            PlayerPrefs.SetInt("Highscore", Highscore);
            PlayerPrefs.Save();
        }
    }

    public void AddOvertimePeriod()
    {
        OvertimePeriods++;
        UpdateOvertimePeriodsUI();
    }

    public void RecordCorrectThrow()
    {
        CorrectlyThrownWastes++;
        ConsecutiveCorrectThrows++;

        bool updatedUI = false; // Flag per tenere traccia se l'UI dello streak è stata aggiornata

        // Esegui le azioni specifiche per gli streak di 3, 5, e 10
        if (ConsecutiveCorrectThrows == 3 || ConsecutiveCorrectThrows == 5 || ConsecutiveCorrectThrows == 10)
        {
            int compliments = Random.Range(1, 6);
            Compliment(compliments);
            alreadystreak = true;

            int pointsToAdd = (ConsecutiveCorrectThrows == 3) ? 30 : 50;
            int startAddPoints = punteggio;
            punteggio = pointsToAdd;
            AddScore();

            punteggio = startAddPoints;
            if (ConsecutiveCorrectThrows == 5 || ConsecutiveCorrectThrows == 10)
            {
                BatteryController.Instance.CollectBattery(10);
            }

            // Evidenzia lo sfondo dello streak per la durata specificata
            UIController.Instance.ShowMaxStreak(ConsecutiveCorrectThrows, true);
            updatedUI = true;
        }

        // Aggiorna il massimo streak raggiunto solo se l'attuale è maggiore del massimo precedentemente registrato
        if (ConsecutiveCorrectThrows > HighestConsecutiveCorrectThrows)
        {
            HighestConsecutiveCorrectThrows = ConsecutiveCorrectThrows;
            if (!updatedUI) // Aggiorna solo se non è già stato aggiornato con un highlight
            {
                UIController.Instance.ShowMaxStreak(HighestConsecutiveCorrectThrows);
            }
        }
        else if (!updatedUI) // Aggiorna l'UI con il massimo streak attuale se non ci sono nuovi record
        {
            UIController.Instance.ShowMaxStreak(HighestConsecutiveCorrectThrows);
        }
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

    private void UpdateScoreUI(int addedScore)
    {
        UIController.Instance.SetScore(Score,addedScore);
    }

    private void UpdateOvertimePeriodsUI()
    {
        UIController.Instance.SetOvertimePeriods(OvertimePeriods);
    }

    public string CalculateGrade()
    {
        UpdateHighscore();
        RewardCoins();
        PlayerPrefs.Save();
        AudioController.Instance.PlayGameOverSound();

        leaderboardScoreManager.SubmitScore(Score);

        if (CorrectlyThrownWastes + MissedWastes == 0)
            return "N/A"; // No throws were made

        float correctPercentage = (float)CorrectlyThrownWastes / (CorrectlyThrownWastes + MissedWastes) * 100;

        if (correctPercentage >= 90)
        {
            return "S";
        }
        else if (correctPercentage >= 85)
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
        else if (correctPercentage >= 50)
        {
            return "E";
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
    //private void Update()
    //{
    //    Debug.Log(punteggio);
    //}

}
