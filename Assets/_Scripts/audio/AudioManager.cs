using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonType<AudioManager>
{
    // Enum with the audioClips. The enum value is equal to audioclip
    public enum AvailableAudioClips
    {
        AudioClip1 = 0
    }

    private List<AudioClip> audios = new List<AudioClip>();

    private AudioSource audioSource;

    private bool isAUdioLibraryReady = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        LoadAudioClips();
    }

    public void PlayAudio(AvailableAudioClips clipName)
    {
        if (audioSource != null && isAUdioLibraryReady)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.PlayOneShot(audios[(int)clipName]);
        }
    }

    public void PlayAudio(int audioIndex)
    {
        if (audioSource != null && isAUdioLibraryReady)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.PlayOneShot(audios[audioIndex]);
        }
    }


    public void StopAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void LoadAudioClips()
    {
        foreach(var value in Enum.GetValues(typeof(AvailableAudioClips)))
        {
            audios.Add(Resources.Load<AudioClip>(value.ToString()));
        }
        // Library loaded
        isAUdioLibraryReady = true;
    }
}
