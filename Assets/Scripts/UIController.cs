using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoSingleton<UIController>
{
    public GameObject PauseButton;
    public GameObject ResumeButton;

    public TMP_Text scoreText;
    public TMP_Text overtimePeriodsText;

    public Image energySlider;

    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public bool IsGameOver;
    public TMP_Text finalScoreText;
    public TMP_Text correctThrowsText;
    public TMP_Text missedThrowsText;
    public TMP_Text gradeText;
    public TMP_Text streakFeedbackText;

    private void Start()
    {
        energySlider.fillAmount = 1f;
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        IsGameOver = false;
        ShowMaxStreak();
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
        }
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
        PlayerPrefs.SetInt("score", TestMissions.Instance.Counter);
        PlayerPrefs.Save();
        Debug.Log("Score saved to PlayerPrefs: " + TestMissions.Instance.Counter);

    }
    public void Quit()
    {
       
        //#if UNITY_EDITOR
        //    UnityEditor.EditorApplication.isPlaying = false;
        //#else
        //    Application.Quit();
        //#endif
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

        HidePausePanel();
        HidePauseButton();
    }

    public void ShowMaxStreak(int streakCount=0)
    {
        streakFeedbackText.text = "Max streak: " + streakCount;
    }


    public void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }

    public void ShowPausePanel()
    {
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
}
