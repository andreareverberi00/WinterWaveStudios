using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    [SerializeField] private AudioSource soundEffectSource;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private string gameOverClipName = "GameOver"; // Assicurati di avere un AudioClip con questo nome

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opzionale, per mantenere l'audio attraverso le scene se necessario
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("MusicEnabled", 1) == 0)
        {
            StopAllSounds();
        }
    }
    public void PlaySound(string clipName)
    {
        AudioClip clip = System.Array.Find(audioClips, item => item.name == clipName);
        if (clip != null)
        {
            if (soundEffectSource.isPlaying && clipName != gameOverClipName)
            {
                soundEffectSource.Stop();
            }
            soundEffectSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("AudioClip not found: " + clipName);
        }
    }

    public void PlayGameOverSound()
    {
        StopAllSounds(); // Ferma tutti i suoni prima di suonare il game over
        PlaySound(gameOverClipName);
    }

    public void StopSound()
    {
        soundEffectSource.Stop();
    }

    public void StopAllSounds()
    {
        // Ferma qualsiasi AudioSource nella scena, si assume che tutti gli AudioController condividano questo metodo
        foreach (var audioSource in FindObjectsOfType<AudioSource>())
        {
            if (audioSource == soundEffectSource) continue;
            audioSource.Stop();
        }
    }
}
