using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI; 
    public GameObject pauseButton;
    public PlayerSlingshot player;

    private GameObject firstActiveGameObject;

    public void Pause()
    {
        player.Animator.SetBool("IsCrouching", false);
        player.enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        pauseButton.SetActive(false);
        LiveTimer.stopTimer = true;
    }

    public void Resume(GameObject thisGO)
    {
        thisGO.SetActive(false);
        player.enabled = true;
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseButton.SetActive(true);
        LiveTimer.stopTimer = false;
    }

    public void Back(GameObject thisGO)
    {
        thisGO.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void Open(GameObject thisGO)
    {
        pauseMenuUI.SetActive(false);
        thisGO.SetActive(true);
    }

    public void RestartRun()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
        LiveTimer.timer = 0;
        LiveTimer.startTime = 0;
        LiveTimer.timerTicking = false;
        LiveTimer.stopTimer = false;
        Stats.fallsCount = 0;
        Stats.jumpsConut = 0;
    }
}
