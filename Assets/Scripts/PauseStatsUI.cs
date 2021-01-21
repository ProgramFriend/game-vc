using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseStatsUI : MonoBehaviour
{
    public TextMeshProUGUI totKillsText;
    public TextMeshProUGUI totDeathsText;
    public Player player;

    public void UpdateStatsUI()
    {
        totKillsText.text = "Total kills: " + player.TotalKills.ToString();
        totDeathsText.text = "Total deaths: " + player.TotalDeaths.ToString();
    }
}
