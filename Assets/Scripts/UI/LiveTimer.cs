using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LiveTimer : MonoBehaviour
{
    public static TextMeshProUGUI timerText;

    public static string text;
    public static float timer = 0;
    public static float startTime = 0;
    public static float savedTime = 0;
    public static bool timerTicking = false;

    /// Start is called on the frame when a script is enabled just before
    void Start()
    {
        timerText = gameObject.GetComponent<TextMeshProUGUI>();
        SaveData.Current.OnLoadGame();
        savedTime = SaveData.Current.GetTimer();
        timer += savedTime;
    }

    // Update is called once per frame
    void Update()
    {   
        string hours = "00";
        string minutes = "00";
        string seconds = "00";

        if (savedTime != 0)
        {
            hours = ((int) savedTime/3600%24).ToString("00");
            minutes = ((int) savedTime / 60).ToString("00");
            seconds = Mathf.Floor((savedTime % 60)).ToString("00");
        }

        if(!SettingsMenu.liveTimer)
        {
            timerText.enabled = false;
        }else
        {
            timerText.enabled = true;
        }

        if(startTime != 0)
        {
            timer = Time.time - startTime + savedTime;
            hours = ((int) timer/3600%24).ToString("00");
            minutes = ((int) timer / 60).ToString("00");
            seconds = Mathf.Floor((timer % 60)).ToString("00");
        }

        text = hours + ":" + minutes + ":" + seconds;
        timerText.text = text;
    }
}
