using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// EQUIPS WEAPON FOR PLAYER
/// DOES PERFORM ATTACK FUNCTION
/// </summary>

public class MCWeaponController : MonoBehaviour
{
    //You can do it for any item: for example weapons, armour, runes, rings, etc...
    public GameObject playerHand; //thats sword parent will be
    public GameObject EquippedWeaponGO { get; set; }

    IWeapon equippedWeapon;
    CharacterStats characterStats;

    public float NextAttackTime { get; set; }

    public GameObject statsPanel;

    //Stats text
    private TextMeshProUGUI Atk;
    private TextMeshProUGUI Def;

    private void Start()
    {
        characterStats = GetComponent<Player>().characterStats;

        Atk = statsPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        Def = statsPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        UpdatePlayerStats();
    }

    public void EquipWeapon(Item itemToEquip)
    {
        /// FIRST PART:
        if (EquippedWeaponGO != null)
        {
            characterStats.RemoveStatBonus(EquippedWeaponGO.GetComponent<IWeapon>().Stats);
            Destroy(playerHand.transform.GetChild(0).gameObject);
        } //if player already has some weapon equipped

        EquippedWeaponGO = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Weapons/" + itemToEquip.ObjectSlug),
            playerHand.transform.position, playerHand.transform.rotation);

        equippedWeapon = EquippedWeaponGO.GetComponent<IWeapon>();
        EquippedWeaponGO.transform.SetParent(playerHand.transform);

        //EquippedWeaponGO.SetActive(false);

        //EquippedWeapon = (GameObject)Instantiate(Resources.Load<GameObject>(ItemType + "/" + itemToEquip.ObjectSlug));

        //need this to know what stats to remove on unequipping
        equippedWeapon.Stats = itemToEquip.Stats;

        //Add Stats to MC Stats
        characterStats.AddStatBonus(itemToEquip.Stats);

        //Debug.Log(itemToEquip.ItemName + " was equipped. Stats[0]: " + equippedWeapon.Stats[0].GetStatValue());
        //Debug.Log("Player stats:" + characterStats.GetStat(StatBase.StatBaseType.Strength).GetStatValue());
        UpdatePlayerStats();
    }

    public void UnequipWeapon()
    {
        Debug.Log("MC weapo ctrl. Unequip");
        characterStats.RemoveStatBonus(EquippedWeaponGO.GetComponent<IWeapon>().Stats);
        Destroy(playerHand.transform.GetChild(0).gameObject);
        UpdatePlayerStats();
    }

    public void Update()
    {
        if (Time.time > NextAttackTime)
        {
            if (Input.GetAxisRaw("Fire1") == 1)
            {
                //include here animation

                //PerformWeaponAttack();
                NextAttackTime = Time.time + 1f;
            }
        }
    }

    public void PerformWeaponAttack()
    {
        // Actual animation playing will be added to individual item scripts;
        equippedWeapon.PerformAttack();
    }
    public void PerformWeaponSpecialAttack()
    {
        // Actual animation playing will be added to individual item scripts;
        Debug.Log("Performing SPECIAL attack from weapon controller");
        equippedWeapon.PerformSpecialAttack();
    }

    public void UpdatePlayerStats()
    {
        Atk.text = "Strength: " + characterStats.GetStat(StatBase.StatBaseType.Strength).GetStatValue();
        Def.text = "Toughness: " + characterStats.GetStat(StatBase.StatBaseType.Toughness).GetStatValue();
    }
}
