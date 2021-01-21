using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// 1. tooltip activation and deactivation
/// 2. drag and drop events
/// 3. item description/ item menu activation and deactivation
/// </summary>

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public int amount;
    public int slotID;

    private Inventory inventory;
    private Tooltip tooltip;
    private InventorySlot inventorySlot;

    private Transform slotTransform;

    private Vector2 offset;

    public bool CanInteract { get; set; }

    void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        tooltip = inventory.GetComponent<Tooltip>();
        inventorySlot = GetComponentInParent<InventorySlot>();

        CanInteract = false;
    }
    void Update()
    {
        if (CanInteract && Input.GetKeyDown(KeyCode.E)) ItemInteract();
        else if (CanInteract && Input.GetKeyDown(KeyCode.P)) ItemDelete();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null && slotID != 16 && slotID != 17 && slotID != 18)
        {
            this.transform.SetParent(this.transform.parent.parent);
            offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
            this.transform.position = eventData.position - offset;
            this.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (item != null && slotID != 16 && slotID != 17 && slotID != 18)
        {
            this.transform.position = eventData.position - offset;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (slotID != 16 && slotID != 17 && slotID != 18)
        {
            slotTransform = inventory.slots[slotID].transform;
            this.transform.SetParent(slotTransform);
            this.transform.position = slotTransform.position;
            this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        CanInteract = true;
        tooltip.Activate(item);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        CanInteract = false;
        tooltip.Deactivate();
    }
    public void ItemInteract()
    {
        if (item.ItemType == Item.ItemTypes.Consumable)
        {
            InventoryController.Instance.ConsumeItem(item);
            ItemDelete();
        }else if(item.ItemType == Item.ItemTypes.Weapon && this.slotID != 13)
        {
            InventoryController.Instance.EquipItem(item);
            inventorySlot.EquipThisItem(this, 13);
            inventory.AddEquippedItem(item);
            CanInteract = false;
        }
        else if (item.ItemType == Item.ItemTypes.Weapon && this.slotID == 13)
        {
            UnequipItem();
        }
    }

    public void UnequipItem()
    {
        if (item.ItemType == Item.ItemTypes.Weapon)
        {
            Debug.Log("Unequip item with letter E");
            inventory.AddItem(item);
            InventoryController.Instance.UnequipItem();
            inventory.items[13] = new Item();
            tooltip.Deactivate();
            inventory.ItemDelete(13);
            inventory.ItemDelete(16);
        }
    }

    public void ItemDelete()
    {
        if(this.amount > 0)
        {
            amount--;
            this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (this.amount+1).ToString();
        }
        else
        {
            tooltip.Deactivate();
            inventory.ItemDelete(slotID);
            //inventory.items[slotID] = new Item();
            Destroy(this.gameObject);
        }
    }
}
