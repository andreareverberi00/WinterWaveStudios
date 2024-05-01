using UnityEngine;

public class BatteryController : MonoSingleton<BatteryController>
{
    public int currentEnergy { get; private set; }
    public int maxEnergy = 100;
    public int consumeEnergyAmount = 5;

    public float energyDrainRate = 1f;
    public float energyDrainInterval = 1f;

    public bool infiniteEnergy = false;

    //public LeaderboardScoreManager leaderboardScoreManager;

    private void Start()
    {
        currentEnergy = maxEnergy;
        InvokeRepeating("DrainEnergy", energyDrainInterval, energyDrainInterval);

        DontDestroyOnLoad(this.gameObject); //Check if this is necessary

        if (infiniteEnergy)
        {
            maxEnergy = 100;
            energyDrainRate = 0;
            consumeEnergyAmount = 0;
        }
    }
    private void DrainEnergy()
    {
        currentEnergy = Mathf.Max(currentEnergy - Mathf.RoundToInt(energyDrainRate * energyDrainInterval), 0);
        UIController.Instance.UpdateEnergy(currentEnergy);
        CheckForGameOver();
    }

    public void CollectBattery(int energyAmount)
    {
        currentEnergy = Mathf.Min(currentEnergy + energyAmount, maxEnergy);
        UIController.Instance.UpdateEnergy(currentEnergy);
        AudioController.Instance.PlaySound("Battery_bar_increase");
        CheckForGameOver();
    }

    private void CheckForGameOver()
    {
        if (currentEnergy <= 0)
        {
            // Al termine della partita
            int finalScore = ScoreController.Instance.Score;
            int correctThrows = ScoreController.Instance.CorrectlyThrownWastes;
            int missedThrows = ScoreController.Instance.MissedWastes;
            string grade = ScoreController.Instance.CalculateGrade();

            UIController.Instance.ShowGameOverPanel(finalScore, correctThrows, missedThrows, grade);

            //leaderboardScoreManager.SubmitScore(finalScore); // Invia il punteggio alla leaderboard

        }
    }
    /*public void Awake()
    {
     
    }*/
    public void ConsumeEnergy()
    {
        currentEnergy = Mathf.Max(currentEnergy - consumeEnergyAmount, 0);
        UIController.Instance.UpdateEnergy(currentEnergy);
        CheckForGameOver();
    }
}
