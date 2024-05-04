using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BatteryController : MonoSingleton<BatteryController>
{
    public int currentEnergy { get; private set; }
    public int maxEnergy = 100;
    public int consumeEnergyAmount = 5;

    public float energyDrainRate = 1f;
    public float energyDrainInterval = 1f;
    public float transitionDuration = 1.0f; // Durata della transizione in secondi

    public bool infiniteEnergy = false;

    private void Start()
    {
        currentEnergy = maxEnergy;
        InvokeRepeating("StartDrainEnergy", energyDrainInterval, energyDrainInterval);

        DontDestroyOnLoad(this.gameObject); //Check if this is necessary

        if (infiniteEnergy)
        {
            maxEnergy = 100;
            energyDrainRate = 0;
            consumeEnergyAmount = 0;
        }
    }

    private void StartDrainEnergy()
    {
        StartCoroutine(DrainEnergy());
    }

    private IEnumerator DrainEnergy()
    {
        int startEnergy = currentEnergy;
        int targetEnergy = Mathf.Max(currentEnergy - Mathf.RoundToInt(energyDrainRate * energyDrainInterval), 0);

        float startTime = Time.time;
        float endTime = startTime + transitionDuration;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / transitionDuration;
            currentEnergy = Mathf.RoundToInt(Mathf.Lerp(startEnergy, targetEnergy, t));
            UIController.Instance.UpdateEnergy(currentEnergy);
            yield return null;
        }

        currentEnergy = targetEnergy; // Assicura che il valore finale sia corretto
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
