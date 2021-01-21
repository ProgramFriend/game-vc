using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionGoal : Goal
{
    public string itemID { get; set; }

    public CollectionGoal(Quest quest, string itemID, string description, bool completed, int curAmount, int reqAmount)
    {
        this.Quest = quest;
        this.itemID = itemID;
        this.Description = description;
        this.Completed = completed;
        this.CurAmount = curAmount;
        this.ReqAmount = reqAmount;
    }

    public override void Init()
    {
        base.Init();
        //EventHandler.OnEnemyDeath += EnemyDied;
        //UIEventHandler.OnItemAddedToInventory += ItemCollected;
    }

    /*void ItemCollected(Item item)
    {
        if (item.ObjectSlug == this.itemID)
        {
            Debug.Log("Item collected: " + itemID);
            this.CurAmount++;
            Evaluate();
        }
    }*/

}
