using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI; 
    public GameObject pauseButton;
    public PlayerSlingshot player;

    private GameObject firstActiveGameObject;

    void Update()
    {
        // if (Input.GetKey(KeyCode.Escape) && GameIsPaused)
        // {
        //     if(pauseMenuUI.activeSelf)
        //     {  
        //         Resume(pauseMenuUI);
        //     }else
        //     {
        //          for (int i = 0; i < gameObject.transform.childCount; i++)
        //         {
        //             if(gameObject.transform.GetChild(i).gameObject.activeSelf == true)
        //             {
        //                     firstActiveGameObject = gameObject.transform.GetChild(1).gameObject;
        //             }
        //         }
        //         Back(firstActiveGameObject);
        //     }
        // }
    }

    public void Pause()
    {
        player.Animator.SetBool("IsCrouching", false);
        player.enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        pauseButton.SetActive(false);
        Stats.stopTimer = true;
    }

    public void Resume(GameObject thisGO)
    {
        thisGO.SetActive(false);
        player.enabled = true;
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseButton.SetActive(true);
        Stats.stopTimer = false;
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
}
