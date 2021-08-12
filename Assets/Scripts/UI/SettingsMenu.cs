using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public Toggle liveTimerEnabled;
    public Slider musicVolume;
    public Slider sfxVolume;

    public AudioMixer musicAudioMixer;
    public AudioMixer sfxAudioMixer;

    public static bool liveTimer = false;

    public void SetMusicVolume(float volume)
    {
        musicAudioMixer.SetFloat("MusicVolume", volume);
        SaveData.Current.SetMusicVolume(volume);
        SerializationManager.Save(SaveData.Current);
    }

    public void SetSFXVolume(float volume)
    {
        sfxAudioMixer.SetFloat("SFXVolume", volume);
        SaveData.Current.SetSfxVolume(volume);
        SerializationManager.Save(SaveData.Current);
    }

    public void SetLiveTimer(bool isLiveTimer)
    {
        liveTimer = isLiveTimer;
        SaveData.Current.SetLivetimerEnabled(isLiveTimer);
        SerializationManager.Save(SaveData.Current);
    }

    public void LoadSettings()
    {
        liveTimerEnabled.isOn = SaveData.Current.settingsData.liveTimerEnabled;
        musicVolume.value = SaveData.Current.settingsData.musicVolume;
        sfxVolume.value = SaveData.Current.settingsData.sfxVolume;
    }
}
