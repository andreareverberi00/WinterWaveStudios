using System.Collections;
using UnityEngine;

public class GameController : MonoSingleton<GameController>
{
    [SerializeField] private GameObject[] bins; 
    [SerializeField] private GameObject conveyorBelt; 

    [SerializeField] private int scoreToAdd = 10; 
    private int score = 0;
    private bool isPaused = false;
    
    public float timeToAddFirstBin = 10f;
    public float timeToAddSecondBin = 20f;
    public float timeToAddConveyorBelt = 30f;
    public float timeToAddThirdBin = 40f;

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

        StartCoroutine(ActivateBin(timeToAddFirstBin, 2)); 
        StartCoroutine(ActivateBin(timeToAddSecondBin, 3)); 
        StartCoroutine(ActivateSecondConveyorBelt(timeToAddConveyorBelt)); 
        StartCoroutine(ActivateBin(timeToAddThirdBin, 4)); 
    }

    IEnumerator ActivateBin(float delay, int binIndex)
    {
        if (binIndex < bins.Length)
        {
            yield return new WaitForSeconds(delay);
            bins[binIndex].SetActive(true);
            WasteController.Instance.ActivateBin(bins[binIndex].GetComponentInChildren<BinDataHolder>());
        }
    }


    IEnumerator ActivateSecondConveyorBelt(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Es: conveyorBelt.ActivateSecondBelt();
    }

    public void AddScore()
    {
        score += scoreToAdd;
        UIController.Instance.SetScore(score);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        UIController.Instance.ShowResumeButton();
        UIController.Instance.HidePauseButton();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        UIController.Instance.ShowPauseButton();
        UIController.Instance.HideResumeButton();
    }
}
