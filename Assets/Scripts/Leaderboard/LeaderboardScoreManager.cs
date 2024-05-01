using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class LeaderboardScoreManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputName;

    public UnityEvent<string, int> submitScoreEvent;
    string playerName;

    public void SubmitScore(int finalScore)
    {
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            playerName = PlayerPrefs.GetString("PlayerName");
        }

        submitScoreEvent.Invoke(playerName, finalScore);
    }
}
