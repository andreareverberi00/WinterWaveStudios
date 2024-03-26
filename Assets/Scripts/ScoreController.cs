using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static ScoreController Instance { get; private set; }
    public int Score { get; private set; }

    public int amount;
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
        Score = 0; // Inizializza il punteggio a 0
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
            Score = 0; // Impedisce al punteggio di scendere sotto lo 0
        }
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        // Aggiorna l'UI del punteggio
        UIController.Instance.SetScore(Score);
        Debug.Log("Score: " + Score);
    }
}
