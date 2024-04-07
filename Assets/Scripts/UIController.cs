using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoSingleton<UIController>
{
    public GameObject PauseButton;
    public GameObject ResumeButton;

    public TMP_Text scoreText;
    public Slider energySlider;

    public GameObject gameOverPanel;
    public TMP_Text finalScoreText;
    public TMP_Text correctThrowsText;
    public TMP_Text missedThrowsText;
    public TMP_Text gradeText;

    private void Start()
    {
        energySlider.value=energySlider.maxValue;
        gameOverPanel.SetActive(false);
    }

    public void SetScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
    public void UpdateEnergy(int newEnergy)
    {
        if (energySlider != null)
        {
            energySlider.value = newEnergy;
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

    public void HidePauseButton()
    { PauseButton.SetActive(false); }
    public void ShowPauseButton()
    { PauseButton.SetActive(true); }
    public void HideResumeButton()
    { ResumeButton.SetActive(false); }
    public void ShowResumeButton()
    { ResumeButton.SetActive(true); }
}
