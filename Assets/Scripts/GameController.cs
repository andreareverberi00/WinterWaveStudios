using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoSingleton<GameController>
{

    //public GameObject RbotPrefab;
    // private Player player;
    //public Player Player { get { return player; } }
    private bool isPaused = false;
    private void Start()
    {
        StartNewGame();
        UIController.Instance.ShowPauseButton();
        UIController.Instance.HideResumeButton();
    }

    void StartNewGame()
    {
        CreateRobot();
        ConveyorBeltController.Instance.CreateAPiece();
    }

    void CreateRobot()
    {
        //GameObject robot = Instantiate(RbotPrefab, Vector3.zero, Quaternion.identity);
        //player = playerGO.GetComponent<Player>();
    }
    private void QuitGame()
    {

    }
    public void PauseGame()
    {
        Time.timeScale = 0; // Ferma il tempo nel gioco
        isPaused = true;
        UIController.Instance.ShowResumeButton();
        UIController.Instance.HidePauseButton();
    }
    public void ResumeGame()
    {
        Time.timeScale = 1; // Riprende il tempo nel gioco
        isPaused = false;
        UIController.Instance.ShowPauseButton();
        UIController.Instance.HideResumeButton();

    }

}

