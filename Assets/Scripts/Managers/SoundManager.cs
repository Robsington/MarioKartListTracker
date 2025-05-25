using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager instance = null;
    public AudioSource audioSource = null;
    [Header("cup audio files")]
    public AudioClip marioStarEarnedClip = null;
    public AudioClip zeldaEarnedClip = null;
    public AudioClip ac_PartyClip = null;
    [Header("track audio files")]
    public AudioClip coinAudio = null;
    public AudioClip ruppeeAudio = null;
    public AudioClip ac_ExclaimAudio = null;
    public AudioClip sixtyNineSound = null;
    public AudioClip finalSound = null;

    void Start()
    {
        instance = this;
    }

    public void SetVolume(SettingsData currentSettings)
    {
        audioSource.volume = currentSettings.volume;
    }
    public void PlaySixtyNineSFX()
    {
        //audioSource.clip = sixtyNineSound;
        //audioSource.Play();
    }

    public void PlayCupCompleteSoundEffect(string cupName)
    {
        if (cupName == "Triforce Cup")
            audioSource.clip = zeldaEarnedClip;
        else if (cupName == "Crossing Cup")
            audioSource.clip = ac_PartyClip;
        else
            audioSource.clip = marioStarEarnedClip;

        audioSource.Play();
    }
    public void PlayTrackCompleteSoundEffect(string cupName)
    {
        if (cupName == "Triforce Cup")
            audioSource.clip = ruppeeAudio;
        else if (cupName == "Crossing Cup")
            audioSource.clip = ac_ExclaimAudio;
        else
            audioSource.clip = coinAudio;

        audioSource.Play();
    }

    public void PlayAllCompletedSoundEffect()
    {
        audioSource.clip = finalSound;
        audioSource.Stop();
        audioSource.Play();
    }
}
