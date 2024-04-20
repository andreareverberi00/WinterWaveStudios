using UnityEngine;

public class BatteryController : MonoSingleton<BatteryController>
{
    public int currentEnergy { get; private set; }
    public int maxEnergy = 100;
    public int consumeEnergyAmount = 20;

    public float energyDrainRate = 1f;
    public float energyDrainInterval = 1f;

    private void Start()
    {
        currentEnergy = maxEnergy;
        InvokeRepeating("DrainEnergy", energyDrainInterval, energyDrainInterval);
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

        }
    }

    public void ConsumeEnergy()
    {
        currentEnergy = Mathf.Max(currentEnergy - consumeEnergyAmount, 0);
        UIController.Instance.UpdateEnergy(currentEnergy);
        CheckForGameOver();
    }
}
