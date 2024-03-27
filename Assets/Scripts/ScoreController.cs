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
        Score = 0; 
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
            Score = 0;
        }
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        UIController.Instance.SetScore(Score);
        Debug.Log("Score: " + Score);
    }
}
