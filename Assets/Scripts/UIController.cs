using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;

public class UIController : MonoSingleton<UIController>
{
    public static new UIController Instance { get; private set; }

    public GameObject PauseButton;
    public GameObject ResumeButton;

    public TMP_Text scoreText;

    private void Awake()
    {
        Instance = this;
    }

    public void SetScore(int score)
    {
        scoreText.text = "Score: " + score;
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
