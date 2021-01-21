using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstQuest : Quest
{
    private void Start()
    {
        QuestName = "FirstQuest";
        Description = "Kill two slimes";
        beforeQuest = new string[] { "KILL", "2", "SLIMES", "Otherwise we will all die" };
        inProgressDialogue = new string[] { "I dont wanna die", "Kill those slimes, faster" };
        rewardDialogue = new string[] {"Wow, you did it!", "I will give you reward, hehe" };
        completedDialogue = new string[] {"Thanks for saving the world!", "I'll never forget you:3"};
        //ItemReward = ItemDatabase.Instance.GetItem("item_name");
        ExpReward = 100;
        GoldReward = 30;

        Goals.Add(new KillGoal(this, 0, "Kill 2 slimes", false, 0, 1)); //somewhy min goal is 2, and auto adds +1

        Goals.ForEach(g => g.Init());
    }
}
