using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public NPCTxtData nPCTxtData;
    public GameObject pauseMenuUI; 
    public GameObject pauseButton;
    public PlayerSlingshot player;

    // private GameObject firstActiveGameObject;

    public void Pause()
    {
        player.Animator.SetBool("IsCrouching", false);
        player.enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        pauseButton.SetActive(false);
    }

    public void Resume(GameObject thisGO)
    {
        thisGO.SetActive(false);
        player.enabled = true;
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseButton.SetActive(true);
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
        player.transform.position = new Vector3(1.18f, -4.681f, 0f);
        player.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        LiveTimer.timer = 0;
        LiveTimer.startTime = 0;
        LiveTimer.savedTime = 0;
        LiveTimer.timerTicking = false;
        Stats.fallsCount = 0;
        Stats.jumpsCount = 0;

        nPCTxtData.ResetNPCTxts();

        var NPCS = FindObjectsOfType<NpcTxt>();
        for(int i = 0; i < NPCS.Length; i++)
        {
            NPCS[i].ResetNPC();
        }
    }
}
