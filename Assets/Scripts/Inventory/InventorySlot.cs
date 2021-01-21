using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 1. Items drag and drop
/// 2. Change items in inventory
/// 3. item equipping in inventory functions
/// </summary>
public class InventorySlot : MonoBehaviour, IDropHandler
{
    public int id;
    private Inventory inventory;

    void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
        if (id == 12)
        {
            if (droppedItem.item.ItemType == Item.ItemTypes.Armor)
            {
                Debug.Log("Armor");
                LittleMagic(droppedItem);
            }
        }
        else if (id == 13)
        {
            if (droppedItem.item.ItemType == Item.ItemTypes.Weapon)
            {
                Debug.Log("Equip item from dragging");
                EquipThisItem(droppedItem, 13);
                InventoryController.Instance.EquipItem(droppedItem.item);
                inventory.AddEquippedItem(droppedItem.item);
            }
        }
        else if (id == 14)
        {
            if (droppedItem.item.ItemType == Item.ItemTypes.Rune)
            {
                Debug.Log("Rune");
                LittleMagic(droppedItem);
            }
        }
        else
            LittleMagic(droppedItem);
    }

    public void LittleMagic(ItemData droppedItem)
    {
        if (inventory.items[id].ID == -1)
        {
            if ((droppedItem.slotID==13 && inventory.items[droppedItem.slotID].ItemType == Item.ItemTypes.Weapon) || inventory.items[droppedItem.slotID].ItemType == Item.ItemTypes.Rune || inventory.items[droppedItem.slotID].ItemType == Item.ItemTypes.Armor)
            {
                Debug.Log("Unequip item from dragging");
                InventoryController.Instance.UnequipItem();
                inventory.items[droppedItem.slotID] = new Item(); //old slot set to null
                inventory.items[id] = droppedItem.item; //new slot has new item
                droppedItem.slotID = id; //update new slot' id
                inventory.ItemDelete(15);
            }
            else
            {
                Debug.Log("Thissss");
                inventory.items[droppedItem.slotID] = new Item();
                inventory.items[id] = droppedItem.item;
                droppedItem.slotID = id;
            }
        }
        else if (droppedItem.slotID != id)//swap items
        {
            Debug.Log("DOing shit?");
            if (inventory.items[droppedItem.slotID].ItemType != Item.ItemTypes.Weapon && inventory.items[droppedItem.slotID].ItemType != Item.ItemTypes.Rune && inventory.items[droppedItem.slotID].ItemType != Item.ItemTypes.Armor)
            {
                Debug.Log(inventory.items[droppedItem.slotID].ItemType);
                Transform item = this.transform.GetChild(0);
                item.GetComponent<ItemData>().slotID = droppedItem.slotID;
                item.transform.SetParent(inventory.slots[droppedItem.slotID].transform);
                item.transform.position = inventory.slots[droppedItem.slotID].transform.position;

                droppedItem.slotID = id;
                droppedItem.transform.SetParent(this.transform);
                droppedItem.transform.position = this.transform.position;

                inventory.items[droppedItem.slotID] = item.GetComponent<ItemData>().item;
                inventory.items[id] = droppedItem.item;
            }
        }
    }
    public void EquipThisItem(ItemData item, int idOfSlot)
    {
        if (inventory.items[idOfSlot].ID == -1)
        {
            Transform thisItem = item.transform;
            inventory.items[item.slotID] = new Item(); //old slot set to null
            inventory.items[idOfSlot] = item.item; //new slot has new item

            item.slotID = idOfSlot;
            thisItem.SetParent(inventory.slots[idOfSlot].transform);
            thisItem.position = inventory.slots[idOfSlot].transform.position;

        } else if(item.slotID != idOfSlot)
        {
            ///old slot has item of new slot
            Transform oldItem = inventory.slots[idOfSlot].transform.GetChild(0);
            oldItem.GetComponent<ItemData>().slotID = item.slotID; //change id to new' slot
            oldItem.transform.SetParent(inventory.slots[item.slotID].transform); //change parent
            oldItem.transform.position = inventory.slots[item.slotID].transform.position;//change position

            //new slot has new item
            Transform thisItem = item.transform;
            inventory.items[idOfSlot] = item.item;
            item.slotID = idOfSlot;
            thisItem.SetParent(inventory.slots[idOfSlot].transform);
            thisItem.position = inventory.slots[idOfSlot].transform.position;
        }
    }
    
}
