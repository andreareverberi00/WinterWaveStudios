using System.Collections;
using UnityEngine;

public class GameController : MonoSingleton<GameController>
{
    [SerializeField] private GameObject[] bins;
    [SerializeField] private int scoreToAdd = 10;
    private int score = 0;
    public bool slow = false;

    // Punti necessari per attivare ciascun bin
    public int pointsToActivateFirstBin = 50;
    public int pointsToActivateSecondBin = 100;
    public int pointsToActivateThirdBin = 150;

    private void Start()
    {
        StartNewGame();
    }

    void StartNewGame()
    {
        foreach (var bin in bins)
        {
            bin.SetActive(false);
        }

        bins[0].SetActive(true);
        bins[1].SetActive(true);
    }

    public void AddScore()
    {
        score += scoreToAdd;
        CheckAndActivateBins();
    }

    private void CheckAndActivateBins()
    {
        if (score >= pointsToActivateFirstBin && !bins[2].activeSelf)
        {
            bins[2].SetActive(true);
            WasteController.Instance.ActivateBin(bins[2].GetComponentInChildren<BinDataHolder>());
        }
        if (score >= pointsToActivateSecondBin && !bins[3].activeSelf)
        {
            bins[3].SetActive(true);
            WasteController.Instance.ActivateBin(bins[3].GetComponentInChildren<BinDataHolder>());
        }
        if (score >= pointsToActivateThirdBin && !bins[4].activeSelf)
        {
            bins[4].SetActive(true);
            WasteController.Instance.ActivateBin(bins[4].GetComponentInChildren<BinDataHolder>());
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        UIController.Instance.ShowPausePanel();
        UIController.Instance.HidePauseButton();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        UIController.Instance.ShowPauseButton();
        UIController.Instance.HidePausePanel();
    }
}
