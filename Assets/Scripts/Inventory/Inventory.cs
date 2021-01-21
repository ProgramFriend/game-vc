using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ADDS ITEM(icon) TO INVENTORY
/// TOGGLES INVENTORY PANEL
/// </summary>

public class Inventory : MonoBehaviour
{
    GameObject InventoryUI { get; set; }

    private GameObject slotPanel;
    public GameObject inventorySlot;
    public GameObject fixedInventorySlot;
    public GameObject worldSlot;
    public GameObject inventoryItem;

    public GameObject worldSlotPanel;
    public GameObject fixedSlotPanel;
    private GameObject playerHand;
    private GameObject toolTip;
    private PlayerMovement playerMovement;

    bool MenuIsActive { get; set; }
    int SlotAmount { get; set; }

    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    private int offsetVal = 5;

    void Start()
    {
        SlotAmount = 12;

        InventoryUI = GameObject.Find("InventoryUI");
        toolTip = GameObject.Find("InventoryUI/AllStats_Panel/Tooltip");
        slotPanel = GameObject.Find("InventoryUI/AllStats_Panel/Inventory_Panel/Slot_Panel");
        playerHand = GameObject.Find("MC/PlayerHand");
        playerMovement = playerHand.GetComponentInParent<PlayerMovement>();

        for (int i = 0; i < SlotAmount; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].GetComponent<InventorySlot>().id = i;
            slots[i].transform.SetParent(slotPanel.transform);
        }

        //Fixed inventory element:
        for (int i = SlotAmount; i < 16; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(fixedInventorySlot));
            slots[i].GetComponent<InventorySlot>().id = i;
            slots[i].transform.SetParent(fixedSlotPanel.transform);
        }

        //Gameplay inventory element:
        for (int i = 16; i < 19; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(worldSlot));
            slots[i].GetComponent<InventorySlot>().id = i;
            slots[i].transform.SetParent(worldSlotPanel.transform);
            
        }

        EventHandler.OnItemAddedToInventory += AddItem;
        InventoryUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            MenuIsActive = !MenuIsActive;
            InventoryUI.SetActive(MenuIsActive);
            toolTip.SetActive(false);
            worldSlotPanel.SetActive(!MenuIsActive);
            playerHand.SetActive(!MenuIsActive);
            playerMovement.enabled = !MenuIsActive;
        }
    }

    public void AddItem(Item itemToAdd)
    {
        //itemToAdd is the item, which will be added to inventory

        if (itemToAdd.Stackable && IsInInventory(itemToAdd)) // Check if item is eligable to adding(stacking)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == itemToAdd.ID) // check to see if item in inventory==to item you want to add
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;

                    //increase stack number by one
                    data.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (data.amount + 1).ToString();
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++) //check to see if there are any empty slots
            {
                if (items[i].ID == -1) // if slot is empty
                {
                    items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(inventoryItem);
                    itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().slotID = i;
                    itemObj.transform.SetParent(slots[i].transform);
                    itemObj.transform.position = Vector2.zero;

                    itemObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Items/" + itemToAdd.ObjectSlug);
                    itemObj.name = itemToAdd.ItemName;

                    //items.Add(items[i]);

                    RectTransform rt = itemObj.GetComponent<RectTransform>();
                    rt.offsetMin = new Vector2(0, 0);
                    rt.offsetMax = new Vector2(-0, -0);
                    rt.localScale = new Vector3(1f, 1f, 1f);
                    //Implement in the future?
                    //UIEventHandler.ItemAddedToInventory(items[i]);
                    break;
                }
            }
        }
    }

    bool IsInInventory(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == item.ID)
            {
                //Debug.Log("Item: " + items[i].Stackable + " " + items[i].ID + " " + items[i].ItemName + " is in inventory");
                return true;
            }
        }
        return false;
    }

    public void AddEquippedItem(Item itemToAdd)
    {
        if (items[16].ID == -1) // if slot is empty
        {
            items[16] = itemToAdd;

            GameObject itemObj = Instantiate(inventoryItem);
            itemObj.GetComponent<ItemData>().item = itemToAdd;
            itemObj.GetComponent<ItemData>().slotID = 16;
            itemObj.transform.SetParent(slots[16].transform);

            itemObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Items/" + itemToAdd.ObjectSlug);
            itemObj.name = itemToAdd.ItemName;
            itemObj.transform.position = Vector2.zero;
            RectTransform rt = itemObj.GetComponent<RectTransform>();
            rt.offsetMin = new Vector2(offsetVal, offsetVal);
            rt.offsetMax = new Vector2(-offsetVal, -offsetVal);

            //Implement in the future?
            //UIEventHandler.ItemAddedToInventory(items[i]);
        }
    }

    public void ItemDelete (int id)
    {
        GameObject obj = slots[id].transform.GetChild(0).gameObject;
        this.items[id] = new Item();

        //TO SAVE ITEMS: SAVE TO MEMORY INVENTORY ITEMS, IF STACKABLE THAN MULTIPLY BY ITEMDATA.AMOUNT
        Destroy(obj);
    }
}
