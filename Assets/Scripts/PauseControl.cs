using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public GameObject pauseMenu;
    public PauseStatsUI pauseStatsUI;
    public static bool GameIsPaused { get; set; }
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            PauseGame();
            pauseStatsUI.UpdateStatsUI();
            pauseMenu.SetActive(GameIsPaused);
        }
    }

    public static void PauseGame()
    {
        GameIsPaused = !GameIsPaused;
        if (GameIsPaused) Time.timeScale = 0f;
        else Time.timeScale = 1;
    }
}
