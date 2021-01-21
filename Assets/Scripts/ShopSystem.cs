using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSystem : MonoBehaviour
{
    public static ShopSystem Instance { get; set; }

    public GameObject itemContainer;
    public GameObject itemsPanel;
    public GameObject shopUI;

    private Player player;
    private PlayerMovement playerMovement;
    private LevelSystem levelSystem;

    public List<Item> itemsToSell = new List<Item>();

    private int ItemNumber = 3;
    private bool hold = true;
    void Start()
    {
        shopUI.SetActive(false);

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        player = GameObject.Find("MC").GetComponent<Player>();
        playerMovement = player.GetComponent<PlayerMovement>();
        levelSystem = player.GetComponent<LevelSystem>();

        for (int i=0; i < ItemNumber; i++)
        {
            Item thisItem = ItemDatabase.Instance.GetItemByID(i);
            if (thisItem.Sellable)
            {
                itemsToSell.Add(thisItem);
                InstallItem(thisItem);
            }
        }

    }
    private void Update()
    {
        if (shopUI.activeSelf)
        {
            if (Input.GetButtonDown("Submit"))
            {
                if (!hold)
                {
                    playerMovement.enabled = true;
                    shopUI.SetActive(false);
                }
                hold = !hold;
            }
        }
    }

    public void OpenShop()
    {
        shopUI.SetActive(true);
        playerMovement.enabled = false;
    }

    public void InstallItem(Item item)
    {
        GameObject obj = Instantiate(itemContainer);

        obj.transform.SetParent(itemsPanel.transform);
        obj.name = item.ObjectSlug;

        if (levelSystem.Level >= item.Level)
        {
            obj.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Items/" + item.ObjectSlug);
            obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.ItemName;
            obj.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => BuyItem(item));
        }
        else
        {
            obj.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Items/lockedItem");
            obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Unlock lvl: " + item.Level;
        }
        obj.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = item.Cost.ToString();
        
    }

    public void UpdateItems()
    {
        foreach(Item item in itemsToSell)
        {
            if (levelSystem.Level == item.Level)
            {
                GameObject obj = GameObject.Find("GUI/Shop_Canvas/Shop/Scroll_View/Panel/" + item.ObjectSlug);
                obj.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Items/" + item.ObjectSlug);
                obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.ItemName;
                obj.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => BuyItem(item));
            }
        }
    }


    public void BuyItem(Item itemToBuy)
    {
        if(player.gold >= itemToBuy.Cost)
        {
            player.MinusGold(itemToBuy.Cost);
            InventoryController.Instance.GiveItem(itemToBuy.ObjectSlug);
            
        }
    }
}
