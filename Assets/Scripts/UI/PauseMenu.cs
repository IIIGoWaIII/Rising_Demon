using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI; 
    public GameObject pauseButton;
    public PlayerSlingshot player;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && GameIsPaused)
        {
            Resume();
        }
    }

    public void Pause()
    {
        player.enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        pauseButton.SetActive(false);
    }

    public void Resume()
    {
        player.enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseButton.SetActive(true);
    }
}
