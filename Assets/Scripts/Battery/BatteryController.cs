using UnityEngine;

public class BatteryController : MonoBehaviour
{
    public static BatteryController Instance { get; private set; }
    public int currentEnergy { get; private set; }
    public int maxEnergy = 100;
    public int consumeEnergyAmount = 20;

    public float energyDrainRate = 1f;
    public float energyDrainInterval = 1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
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
        CheckForGameOver();
    }

    private void CheckForGameOver()
    {
        if (currentEnergy <= 0)
        {
            // Game Over
            // UIController.Instance.ShowGameOverScreen();
        }
    }

    public void ConsumeEnergy()
    {
        currentEnergy = Mathf.Max(currentEnergy - consumeEnergyAmount, 0);
        UIController.Instance.UpdateEnergy(currentEnergy);
        CheckForGameOver();
    }
}
