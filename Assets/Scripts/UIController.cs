using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class UIController : MonoSingleton<UIController>
{
    public GameObject PauseButton;
    public GameObject ResumeButton;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
