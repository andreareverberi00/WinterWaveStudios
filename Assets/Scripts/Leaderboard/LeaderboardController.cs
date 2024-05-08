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

    private string publicLeaderboardKey = "5fa65fbfee8f647c93f6b873447a123ddd0728b24f8223f727bb5aa8d1cb659e";

    public TMP_InputField playerNameInput;
    string playerName;

    private void Start()
    {
        if(playerNameInput)
            playerNameInput.characterLimit = 10;

        // Carica il nome salvato al caricamento del gioco
        if (PlayerPrefs.HasKey("PlayerName")&&playerNameInput)
        {
            playerNameInput.text = PlayerPrefs.GetString("PlayerName");
        }
        else
        {
            playerName = "Player" + Random.Range(100, 1000);
            if(playerNameInput)
                playerNameInput.text = playerName;
            
            SaveName();
        }
        // Aggiungi un listener per salvare il nome ogni volta che viene modificato
        if (playerNameInput)
            playerNameInput.onValueChanged.AddListener(delegate { SaveName(); });


        if (names.Count>0&&scores.Count>0)
            GetLeaderboard();
    }

    // Cancella i valori salvati
    [ContextMenu("Clear Saved Values")]
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Clear Saved Values");
    }

    void SaveName()
    {
        if (playerNameInput != null)
        {
            string newName = playerNameInput.text;  // Leggi il nuovo nome direttamente dall'input field
            PlayerPrefs.SetString("PlayerName", newName);
            PlayerPrefs.Save();  // Salva le PlayerPrefs
            Debug.Log("Name saved: " + newName);  // Aggiungi un log per confermare il salvataggio
        }
    }


    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) => { 
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            print(loopLength);
            for (int i = 0; i < loopLength; ++i)
            {
                names[i].text = (i+1).ToString()+' '+ msg[i].Username;
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
