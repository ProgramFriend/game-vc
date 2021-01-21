using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryStats : MonoBehaviour
{
    /// <summary>
    /// MOVE THIS SCRIPT TO MC WEAPON CONTROLLER
    /// </summary>
    public GameObject statsPanel;

    //Stats text
    private TextMeshProUGUI Atk;
    private TextMeshProUGUI Def;

    CharacterStats characterStats;

    public string AtkText { get; set; }
    public string DefText { get; set; }
    void Start()
    {
        characterStats = GetComponent<Player>().characterStats;

        Atk = statsPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        Def = statsPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        //Debug.Log("Player statssss:" + characterStats.GetStat(StatBase.StatBaseType.Strength).GetStatValue());
        //Debug.Log(characterStats.GetStat(StatBase.StatBaseType.Strength).GetStatValue());
        
    }
}
