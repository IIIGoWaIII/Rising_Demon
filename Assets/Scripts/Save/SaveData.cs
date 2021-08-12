using System.Collections;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    private static SaveData current;
    public static SaveData Current
    {
        get
        {
            if(current == null)
            {
                current = new SaveData();
            }
            return current;
        }
    }

    public PlayerData playerData = new PlayerData();
    public SettingsData settingsData = new SettingsData();

    public void GetPlayerPosition(GameObject player)
    {
        player.transform.localRotation = Quaternion.Euler(0f, SaveData.current.playerData.rotationY * 180, 0f);
        player.transform.position = new Vector3(SaveData.current.playerData.positionX, SaveData.current.playerData.positionY, SaveData.current.playerData.positionZ);
    }

    public void SetPlayerPosition(Vector3 playerPosition, float rotationY)
    {
        SaveData.current.playerData.positionX = playerPosition.x;
        SaveData.current.playerData.positionY = playerPosition.y;
        SaveData.current.playerData.positionZ = playerPosition.z;

        SaveData.current.playerData.rotationY = rotationY;
    }

    public void SetTimer(float timer)
    {
        SaveData.current.settingsData.timer = timer;
    }

    public float GetTimer()
    {
        return SaveData.current.settingsData.timer;
    }

    public void SetMusicVolume(float musicVolume)
    {
        SaveData.current.settingsData.musicVolume = musicVolume;
    }

    public void SetSfxVolume(float sfxVolume)
    {
        SaveData.current.settingsData.sfxVolume = sfxVolume;
    }

    public float GetMusicVolume()
    {
        return SaveData.current.settingsData.musicVolume;
    }

    public float GetSfxVolume()
    {
        return SaveData.current.settingsData.sfxVolume;
    }

    public void SetLivetimerEnabled(bool livetimerEnabled)
    {
        SaveData.current.settingsData.liveTimerEnabled = livetimerEnabled;
    }

    public bool GetLiveTimerEnabled()
    {
        return SaveData.current.settingsData.liveTimerEnabled;
    }

    public void SetJumpsCount(int jumpsConut)
    {
        SaveData.current.playerData.jumpsCount = jumpsConut;
    }

    public int GetJumpsCount()
    {
        return SaveData.current.playerData.jumpsCount;
    }

    public void SetFallsCount(int fallsConut)
    {
        SaveData.current.playerData.fallsCount = fallsConut;
    }

    public int GetFallsCount()
    {
        return SaveData.current.playerData.fallsCount;
    }

    public void OnLoadGame()
    {
        SaveData.current = (SaveData)SerializationManager.Load();
    }

}
