using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour
{
    private Item item;
    private GameObject tooltip;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI itemDescription;

    void Start()
    {
        tooltip = GameObject.Find("InventoryUI/AllStats_Panel/Tooltip");
        tooltip.SetActive(false);
    }
    void Update()
    {
        if (tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition;
        }
    }

    public void Activate(Item Item)
    {
        this.item = Item;
        ConstructDataString();
        tooltip.SetActive(true);
        tooltip.transform.position = Input.mousePosition;
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }


    public void ConstructDataString()
    {
        itemName = tooltip.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        itemDescription = tooltip.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        itemDescription.text = "";

        itemName.text = item.ItemName;
        itemName.color = new Color(1, 0, 0);

        if(item.Stats != null)
        {
            foreach (StatBase stat in item.Stats)
            {
                itemDescription.text += stat.StatName + ": " + stat.BaseValue + "\n";
            }
        }
        else
        {
            itemDescription.text = item.Description;
        }

        itemDescription.color = new Color(1, 1, 1);
    }
}
