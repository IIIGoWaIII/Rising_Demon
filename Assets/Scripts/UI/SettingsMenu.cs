using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public static AudioMixer musicAudioMixer;
    public static AudioMixer sfxAudioMixer;

    public static bool liveTimer = false;

    public static float musicVolume = 0f;
    public static float sfxVolume = 0f;

    public void SetMusicVolume(float volume)
    {
        musicAudioMixer.SetFloat("MusicVolume", volume);
        musicVolume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxAudioMixer.SetFloat("SFXVolume", volume);
        musicVolume = volume;
    }

    public void setLiveTimer(bool isLiveTimer)
    {
        liveTimer = isLiveTimer;
    }
}
