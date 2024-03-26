using UnityEngine;

public class BatteryController : MonoBehaviour
{
    public static BatteryController Instance { get; private set; }
    public int currentEnergy { get; private set; }
    public int maxEnergy = 100;
    public int consumeEnergyAmount = 20;
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
        currentEnergy = maxEnergy; // Energia iniziale al massimo.

    }

    public void CollectBattery(int energyAmount)
    {
        currentEnergy = Mathf.Min(currentEnergy + energyAmount, maxEnergy);
        UIController.Instance.UpdateEnergy(currentEnergy);

        // Qui, aggiorni l'UI dell'energia. Per esempio:
        // UIController.Instance.UpdateEnergy(currentEnergy);
        CheckForGameOver();
    }

    private void CheckForGameOver()
    {
        if (currentEnergy <= 0)
        {
            // Logica di Game Over
            // UIController.Instance.ShowGameOverScreen();
        }
    }

    // Chiamato quando il giocatore consuma energia.
    public void ConsumeEnergy()
    {
        currentEnergy = Mathf.Max(currentEnergy - consumeEnergyAmount, 0);
        UIController.Instance.UpdateEnergy(currentEnergy);
        // Aggiorna l'UI dell'energia
        // UIController.Instance.UpdateEnergy(currentEnergy);
        CheckForGameOver();
    }
}
