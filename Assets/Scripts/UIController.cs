using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoSingleton<UIController>
{
    public static new UIController Instance { get; private set; }

    public GameObject PauseButton;
    public GameObject ResumeButton;

    public TMP_Text scoreText;
    public Slider energySlider; 

    private void Awake()
    {
        Instance = this;
        energySlider.value=energySlider.maxValue;
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

    public void HidePauseButton()
    { PauseButton.SetActive(false); }
    public void ShowPauseButton()
    { PauseButton.SetActive(true); }
    public void HideResumeButton()
    { ResumeButton.SetActive(false); }
    public void ShowResumeButton()
    { ResumeButton.SetActive(true); }
}
