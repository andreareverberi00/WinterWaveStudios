using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using UnityEngine.UI;

public class LeaderboardController : MonoBehaviour
{
    [SerializeField] 
    private List<TMP_Text> names;
    [SerializeField] 
    private List<TMP_Text> scores;

    private string publicLeaderboardKey = "5e3cd2b5ed3928e4efa6a4d4cc2467f55fda3d9e553344ff251313e80f995f2f";

    public TMP_InputField playerNameInput;
    string playerName;

    private void Start()
    {
        // Carica il nome salvato al caricamento del gioco
        if (PlayerPrefs.HasKey("PlayerName")&&playerNameInput)
        {
            playerNameInput.text = PlayerPrefs.GetString("PlayerName");
        }
        else
        {
            playerName = "Player" + Random.Range(100, 1000);
            playerNameInput.text = playerName;
            PlayerPrefs.SetString("PlayerName", playerName);
            PlayerPrefs.Save();
        }
        // Aggiungi un listener per salvare il nome ogni volta che viene modificato
        if(playerNameInput)
            playerNameInput.onValueChanged.AddListener(SaveName);

        if(names.Count>0&&scores.Count>0)
            GetLeaderboard();
    }

    // Cancella i valori salvati
    [ContextMenu("Clear Saved Values")]
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Clear Saved Values");
    }

    void SaveName(string newName)
    {
        PlayerPrefs.SetString("PlayerName", newName);
        PlayerPrefs.Save();  // Non dimenticare di salvare le PlayerPrefs
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) => { 
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            print(loopLength);
            for (int i = 0; i < loopLength; ++i)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboardEntry(string Username, int Score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, Username, Score, ((msg) =>
        {
            // if contiene brutte parole return
            GetLeaderboard();
        }));
        LeaderboardCreator.ResetPlayer();
    }
}