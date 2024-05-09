using System.Collections;
using System.Security.Claims;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoSingleton<UIController>
{
    [Header("Panels")]
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject optionPanel;
    public GameObject restartPanel;
    public GameObject quitPanel;
    public GameObject suggestionPanel;

    [Space(10)]

    [Header("Texts")]
    public TMP_Text finalScoreText;
    public TMP_Text correctThrowsText;
    public TMP_Text missedThrowsText;
    public TMP_Text gradeText;
    public TMP_Text streakFeedbackText;
    public TMP_Text coinsText;
    public TMP_Text scoreText;
    public TMP_Text overtimePeriodsText;
    [Space(10)]

    [Header("Misc")]
    public GameObject PauseButton;
    public GameObject ResumeButton;
    public PerkController perkcontroller;
    bool quit;
    bool restart;
    public Image energySlider;
    public Image deathBorderImg;
    public Image streakBackgroundImg;

    public GameObject musicButton;
    public bool IsGameOver;

    public float transitionDuration = 1.0f; // Durata della transizione in secondi
    int currentStreakCount = 0;

    private void Start()
    {
        energySlider.fillAmount = 1f;
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        optionPanel.SetActive(false);
        restartPanel.SetActive(false);
        quitPanel.SetActive(false);
        IsGameOver = false;
        ShowMaxStreak();
        AudioListener.pause = PlayerPrefs.GetInt("MusicEnabled", 1) == 1 ? false : true;
        musicButton.GetComponentInChildren<TMP_Text>().text = PlayerPrefs.GetInt("MusicEnabled", 1) == 1 ? "MUSIC: ON" : "MUSIC: OFF";
        if (deathBorderImg != null)
        {
            Color color = deathBorderImg.color;
            color.a = 0f;
            deathBorderImg.color = color;
        }
        StartCoroutine(SuggestionPanel());
    }
    IEnumerator SuggestionPanel()
    {
        yield return new WaitForSeconds(1);
        suggestionPanel.SetActive(true);
        yield return new WaitForSeconds(4);
        suggestionPanel.SetActive(false);
    }
    public void OnMusicButtonPressed()
    {
        ToggleMusic();
    }
    private void ToggleMusic()
    {
        // Leggi lo stato corrente della musica dalle PlayerPrefs (ritorna 0 se non definito)
        bool isEnabled = PlayerPrefs.GetInt("MusicEnabled") == 1;
        print(isEnabled);
        // Cambia lo stato della musica
        isEnabled = !isEnabled;

        // Imposta il volume dell'audio listener
        AudioListener.pause = isEnabled ? false : true;

        // Salva il nuovo stato nelle PlayerPrefs
        PlayerPrefs.SetInt("MusicEnabled", isEnabled ? 1 : 0);

        // Aggiorna il testo del bottone
        musicButton.GetComponentInChildren<TMP_Text>().text = "MUSIC: " + (isEnabled ? "ON" : "OFF");
        PlayerPrefs.Save();
    }

    public void SetScore(int score,int addedScore)
    {
        scoreText.text = score.ToString("0000");
        //StartCoroutine(SetAddedScoreText(addedScore));
    }
    IEnumerator SetAddedScoreText(int addedScore)
    {
        string startText = scoreText.text;
        scoreText.text += "+" + addedScore;
        yield return new WaitForSeconds(2);
        scoreText.text = startText;
    }
    public void SetOvertimePeriods(int overtimePeriods)
    {
        overtimePeriodsText.text = overtimePeriods.ToString();
    }
    public void UpdateEnergy(int newEnergy)
    {
        if (energySlider != null)
        {
            StartCoroutine(UpdateEnergyCoroutine(newEnergy));
        }
    }

    private IEnumerator UpdateEnergyCoroutine(int newEnergy)
    {
        float startTime = Time.time;
        float endTime = startTime + transitionDuration;
        float startAmount = energySlider.fillAmount;
        float targetAmount = Mathf.InverseLerp(0, 100, newEnergy);

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / transitionDuration;
            energySlider.fillAmount = Mathf.Lerp(startAmount, targetAmount, t);
            UpdateDeathBorder(newEnergy);
            yield return null;
        }
        energySlider.fillAmount = targetAmount; // Assicurati che il valore finale sia corretto
        UpdateDeathBorder(newEnergy);
    }

    private void UpdateDeathBorder(int newEnergy)
    {
        if (deathBorderImg != null)
        {
            if (newEnergy <= 50)
            {
                Color color = deathBorderImg.color;
                color.a = Mathf.Lerp(0f, 1f, Mathf.InverseLerp(50, 0, newEnergy));
                deathBorderImg.color = color;
            }
            else
            {
                Color color = deathBorderImg.color;
                color.a = 0;
                deathBorderImg.color = color;
            }
        }
    }


    public void Restart()
    {
        restart = true;
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");

    }
    public void Quit()
    {
        //perkcontroller.nocustom= false;

        quit=true;
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void ShowGameOverPanel(int finalScore, int correctThrows, int missedThrows, string grade)
    {
        gameOverPanel.SetActive(true);
        GameOver();
        finalScoreText.text = "Final Score: " + finalScore;
        correctThrowsText.text = "Correct Throws: " + correctThrows;
        missedThrowsText.text = "Missed Throws: " + missedThrows;
        gradeText.text = "Grade: " + grade;
        coinsText.text = "Coins: "+PlayerPrefs.GetInt("Coins").ToString();
        Time.timeScale = 0;
        HidePausePanel();
        HidePauseButton();
    }

    // Metodo unificato per mostrare lo streak e opzionalmente evidenziare lo sfondo
    public void ShowMaxStreak(int streakCount = 0, bool highlight = false)
    {
        currentStreakCount = streakCount;
        if (highlight)
        {
            // Evidenzia lo sfondo per lo streak attuale
            HighlightStreakBackground(streakCount);
        }
        else
        {
            // Mostra solo lo streak massimo senza evidenziare
            streakFeedbackText.text = "Max streak: " + streakCount;
        }
    }

    private void HighlightStreakBackground(int streakCount)
    {
        StartCoroutine(HighlightStreakBackgroundForSeconds(2f, streakCount));
    }

    private IEnumerator HighlightStreakBackgroundForSeconds(float seconds, int actualStreakCount)
    {
        // Evidenzia lo sfondo e mostra il numero dello streak attuale
        streakBackgroundImg.color = Color.yellow;
        streakFeedbackText.text = "STREAK OF: " + actualStreakCount;

        // Attendi per un periodo specificato
        yield return new WaitForSeconds(seconds);

        // Ripristina lo sfondo e mostra il massimo streak raggiunto
        streakBackgroundImg.color = Color.white;
        streakFeedbackText.text = "Max streak: " + currentStreakCount;
    }
    public void HidePausePanel()
    {
        AudioListener.volume = 1;
        pausePanel.SetActive(false);
    }

    public void ShowPausePanel()
    {
        AudioListener.volume = 0;
        pausePanel.SetActive(true);
    }
    public void HidePauseButton()
    { PauseButton.SetActive(false); }
    public void ShowPauseButton()
    { PauseButton.SetActive(true); }
    void GameOver()
    {
        IsGameOver = true;
    }
    public void HideOptionPanel()
    {
        optionPanel.SetActive(false);
    }
    public void ShowOptionPanel()
    {
        optionPanel.SetActive(true);
    }
    public void HideRestartPanel()
    {
        restartPanel.SetActive(false);
    }
    public void ShowRestartPanel()
    {
        restartPanel.SetActive(true);
    }
    public void HideQuitPanel()
    {
        quitPanel.SetActive(false);
    }
    public void ShowQuitPanel()
    {
        quitPanel.SetActive(true);
    }
    IEnumerator WaitandLoad()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Menu");


    }
    IEnumerator WaitandLoad2()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Main");


    }

    private void Update()
    {
        if(quit==true )
        {
            StartCoroutine(WaitandLoad());
            quit = false;
        }
        if (restart == true)
        {
            StartCoroutine(WaitandLoad2());
            restart = false;
        }
    }
}
