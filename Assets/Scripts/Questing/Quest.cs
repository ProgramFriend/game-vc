using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Quest : MonoBehaviour
{
    //Player player;
    //LevelSystem lvlSys;
    //lvlSys.UpdateXP(ExpReward);
    //player.GiveGold(GoldReward);

    public string[] beforeQuest;
    public string[] inProgressDialogue;
    public string[] rewardDialogue;
    public string[] completedDialogue;
    public List<Goal> Goals { get; set; } = new List<Goal>();
    public string QuestName { get; set; }
    public string Description { get; set; }
    public int ExpReward { get; set; }
    public int GoldReward { get; set; }
    public Item ItemReward {get; set; }
    public bool Completed { get; set; }

    public void CheckGoals()
    {
        Completed = Goals.All(g => g.Completed);  //g stands for goal. If all g will be g.completed, then
    }

    public void GiveReward()
    {
        //if (ItemReward != null) InventoryController.Instance.GiveItem(ItemReward);
        if (GoldReward > 0) EventHandler.GiveGold(GoldReward);
        if (ExpReward > 0) EventHandler.GiveExp(ExpReward);
    }

}
