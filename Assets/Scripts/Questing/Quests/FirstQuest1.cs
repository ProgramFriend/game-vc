using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstQuest1 : Quest
{
    private void Start()
    {
        QuestName = "FirstQuest1";
        Description = "FirstQuest1";
        beforeQuest = new string[] { "So, ill give you second quest", "Kill one more slime", "And you will get labai daug money" };
        inProgressDialogue = new string[] { "Dont you wanna your thousand?", "Kill more slimes" };
        rewardDialogue = new string[] { "I could have done it myself", "You won't get any money"};
        completedDialogue = new string[] {"Thanks for saving the world!!!", "See you around!"};
        //ItemReward = ItemDatabase.Instance.GetItem("item_name");
        ExpReward = 100;

        Goals.Add(new KillGoal(this, 0, "Kill 10000 sLIMES", false, 0, 1)); //somewhy min goal is 2, and auto adds +1

        Goals.ForEach(g => g.Init());
    }
}
