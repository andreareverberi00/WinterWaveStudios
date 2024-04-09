using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIController : MonoSingleton<UIController>
{
    public GameObject PauseButton;
    public GameObject ResumeButton;

    public TMP_Text scoreText;
    public Image energySlider;

    public GameObject gameOverPanel;
    public TMP_Text finalScoreText;
    public TMP_Text correctThrowsText;
    public TMP_Text missedThrowsText;
    public TMP_Text gradeText;
    public TMP_Text streakFeedbackText;

    private void Start()
    {
        energySlider.fillAmount=1f;
        gameOverPanel.SetActive(false);
        streakFeedbackText.gameObject.SetActive(false);
    }

    public void SetScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
    public void UpdateEnergy(int newEnergy)
    {
        if (energySlider != null)
        {
            float clampedEnergy = Mathf.InverseLerp(0, 100, newEnergy);
            print(clampedEnergy);
            energySlider.fillAmount = clampedEnergy;
        }
    }
    public void Restart() 
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ShowGameOverPanel(int finalScore, int correctThrows, int missedThrows, string grade)
    {
        gameOverPanel.SetActive(true); 

        finalScoreText.text = "Final Score: " + finalScore;
        correctThrowsText.text = "Correct Throws: " + correctThrows;
        missedThrowsText.text = "Missed Throws: " + missedThrows;
        gradeText.text = "Grade: " + grade;

        //HidePauseButton();
        //HideResumeButton();
    }
    public void ShowStreakFeedback(int streakCount)
    {
        streakFeedbackText.text = "Streak of " + streakCount + "!";
        streakFeedbackText.gameObject.SetActive(true);

        StartCoroutine(HideStreakFeedback());
    }

    IEnumerator HideStreakFeedback()
    {
        yield return new WaitForSeconds(2f);
        streakFeedbackText.gameObject.SetActive(false);
    }

    public void HidePauseButton()
    { PauseButton.SetActive(false); }
    public void ShowPauseButton()
    { PauseButton.SetActive(true); }
    public void HideResumeButton()
    { ResumeButton.SetActive(false); }
    public void ShowResumeButton()
    { ResumeButton.SetActive(true); }
}
