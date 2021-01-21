using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


/// <summary>
/// BUILDS ITEMS DATABASE OF ITEM SCRIPT
/// </summary>

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance { get; set; }

    private List<Item> items = new List<Item>();

    private void Start()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        BuildDatabase();

    }

    private void BuildDatabase()
    {
        items = JsonConvert.DeserializeObject<List<Item>>(Resources.Load<TextAsset>("JSON/Items").ToString());
    }

    public Item GetItem(string itemSlug)
    {
        foreach(Item item in items)
        {
            if (item.ObjectSlug == itemSlug) 
                return item;
        }
        Debug.Log("I.D.: Couldn't find item: " + itemSlug);
        return null;
    }

    public Item GetItemByID(int id)
    {
        foreach(Item item in items)
        {
            //Debug.Log("PLUS 1 ITEM: " + item.ID + " / " + id);
            if (item.ID == id)
                return item;
        }
        Debug.Log("Couldn't find item of ID: " + id);
        return null;
    }


}
