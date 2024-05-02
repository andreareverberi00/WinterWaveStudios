using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerSpeakController : MonoSingleton<SpeakerSpeakController>
{

    [SerializeField] private AudioSource soundEffectSource;
    [SerializeField] private AudioClip[] audioClips;

    public void PlaySound(string clipName)
    {
        AudioClip clip = System.Array.Find(audioClips, item => item.name == clipName);
        if (clip != null)
        {
            if (soundEffectSource.isPlaying)
            {
                soundEffectSource.Stop();
            }
            soundEffectSource.PlayOneShot(clip);
            VFXController.Instance.PlayVFXAtPosition(VFXType.SpeakerSound, SpeakerController.Instance.GetPosition(), 5f);
        }
        else
        {
            Debug.LogWarning("AudioClip not found: " + clipName);
        }
    }

    public void StopSound()
    {
        soundEffectSource.Stop();
    }
}
