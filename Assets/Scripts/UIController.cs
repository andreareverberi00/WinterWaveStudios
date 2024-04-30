using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoSingleton<UIController>
{
<<<<<<< Updated upstream
    [Header("Panels")]
=======
    public GameObject PauseButton;
    public GameObject ResumeButton;
    public PerkController perkcontroller;
    public TMP_Text scoreText;
    public TMP_Text overtimePeriodsText;
    bool quit;
    bool restart;
    bool Default;
    public Image energySlider;

>>>>>>> Stashed changes
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject optionPanel;
    public GameObject restartPanel;
    public GameObject quitPanel;
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

    public Toggle musicToggle;
    public bool IsGameOver;

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
        musicToggle.isOn = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;

    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString("0000");
    }
    public void SetOvertimePeriods(int overtimePeriods)
    {
        overtimePeriodsText.text = overtimePeriods.ToString();
    }
    public void UpdateEnergy(int newEnergy)
    {
        if (energySlider != null)
        {
            float clampedEnergy = Mathf.InverseLerp(0, 100, newEnergy);
            energySlider.fillAmount = clampedEnergy;
            //if(Default)
            //{ 
            //if((clampedEnergy <= 10))
            //{
            //    //SpeakerController.Instance 
            //}
            //}    
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

        //#if UNITY_EDITOR
        //    UnityEditor.EditorApplication.isPlaying = false;
        //#else
        //    Application.Quit();
        //#endif

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

    public void ShowMaxStreak(int streakCount=0)
    {
        streakFeedbackText.text = "Max streak: " + streakCount;
    }


    public void HidePausePanel()
    {
        //AudioListener.volume = 1;
        pausePanel.SetActive(false);
    }

    public void ShowPausePanel()
    {
        //AudioListener.volume = 0;
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
    public void SetMusicEnabled()
    {
        bool isEnabled = musicToggle.isOn;
        AudioListener.volume = isEnabled ? 1 : 0;
        PlayerPrefs.SetInt("MusicEnabled", isEnabled ? 1 : 0);
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
