using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Data
{
    public bool liveTimer;
    public float timer;
    public float startTime;

    public float musicVolume;
    public float sfxVolume;

    public float[] position;
    public float playerYRotation;

    public Data(SettingsMenu settings)
    {
        liveTimer = SettingsMenu.liveTimer;
        timer = LiveTimer.timer;
        startTime = LiveTimer.startTime;
    }

    public Data(PlayerSlingshot player)
    {
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[3] = player.transform.position.z;
        playerYRotation = player.transform.localRotation.y;
    }

    public Data(float setMusicVolume, float setSfxVolume)
    {
        musicVolume = setMusicVolume;
        sfxVolume = setSfxVolume;
    }
}
