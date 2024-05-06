using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAndShopMusic : MonoBehaviour
{
    public static MenuAndShopMusic Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if(PlayerPrefs.GetInt("MusicEnabled", 1) == 0)
        {
            StopMusic();
        }
    }
    private void Update()
    {
        CheckCurrentScene();
    }
    public void StopMusic()
    {
        gameObject.SetActive(false);
    }
    private void CheckCurrentScene()
    {
        // Lista delle scene in cui non vuoi che la musica suoni
        string[] scenesWithoutMusic = { "Main"};

        foreach (var sceneName in scenesWithoutMusic)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                Destroy(gameObject); // Distruggi l'oggetto se si trova in una "scena silenziosa"
                return; // Interrompi il ciclo e la funzione
            }
        }
    }
}
