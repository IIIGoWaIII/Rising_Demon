using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
    public static float timer = 0;
    public static float startTime = 0;
    public static int jumpsConut = 0;
    public static int fallsCount = 0;
    public static bool timerTicking = false;
    public static bool stopTimer = false;

    private TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = gameObject.GetComponent<TextMeshProUGUI>(); 
    }

    // Update is called once per frame
    void Update()
    {
        string hours = "00";
        string minutes = "00";
        string seconds = "00";

        if(startTime != 0)
        {
            timer = Time.time - startTime;
            hours = ((int) timer/3600%24).ToString("00");
            minutes = ((int) timer / 60).ToString("00");
            seconds = Mathf.Floor((timer % 60)).ToString("00");
        }

        textMesh.text = "Time: " + hours + ":" + minutes + ":" + seconds + "\nJumps: " + jumpsConut.ToString() + "\nFalls: " + fallsCount.ToString();
    }
}
