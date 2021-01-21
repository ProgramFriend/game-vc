using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public GameObject player;

    public GameObject PauseMenuUI;
    public GameObject GodModePanel;

    public bool ToggleValue { get; set; }
    public bool PlayerColliderValue { get; set; }
    public bool EnemiesFollowValue { get; set; }

    public void EnterGodMode()
    {
        ToggleValue = !ToggleValue;
        GodModePanel.SetActive(ToggleValue);
        if (!ToggleValue)
        {
            player.GetComponent<CapsuleCollider2D>().enabled = true;
            player.GetComponent<PlayerMovement>().moveSpeed = 6;
        }
    }

    public void ChangePlayerCollider()
    {
        player.GetComponent<CapsuleCollider2D>().enabled = PlayerColliderValue;
        PlayerColliderValue = !PlayerColliderValue;
    }


    public void OnPlayerSpeedChanged(string _value)
    {
        player.GetComponent<PlayerMovement>().moveSpeed = float.Parse(_value); 
    }


    public void ContinueGame()
    {
        PauseControl.PauseGame();
        PauseMenuUI.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
