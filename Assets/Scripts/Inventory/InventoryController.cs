using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// BUILDS CONSUME AND EQUIP FUNCTIONS TO USE IN INVENTORY ITEM DATA
/// GiveItem: gives item to the player, adds that item to inventory
/// </summary>

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance { get; set; }
    public MCWeaponController mcWeaponController;
    public ConsumablesController consumablesController;

    private void Start()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        mcWeaponController = GetComponent<MCWeaponController>();
        consumablesController = GetComponent<ConsumablesController>();


        GiveItem("sword_0");
        GiveItem("potion_log");
        GiveItem("potion_log");
        GiveItem("potion_log");
        //GiveItem(0);
        //GiveItem(2);
    }

    public void GiveItem(string itemSlug)
    {
        Item item = ItemDatabase.Instance.GetItem(itemSlug);
        EventHandler.ItemAddedToInventory(item);
    }
    public void GiveItem(int itemID)
    {
        Item item = ItemDatabase.Instance.GetItemByID(itemID);
        EventHandler.ItemAddedToInventory(item);
    }

    public void EquipItem(Item itemToEquip)
    {
        mcWeaponController.EquipWeapon(itemToEquip);
    }

    public void UnequipItem()
    {
        mcWeaponController.UnequipWeapon();
    }

    public void DeleteItem()
    {
        
    }

    public void ConsumeItem(Item itemToConsume)
    {
        consumablesController.ConsumeItem(itemToConsume);
    }
}
