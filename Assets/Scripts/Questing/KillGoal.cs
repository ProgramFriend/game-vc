using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : Goal
{
    public int EnemyID { get; set; }

    public KillGoal(Quest quest, int enemyID, string description, bool completed, int curAmount, int reqAmount)
    {
        this.Quest = quest;
        this.EnemyID = enemyID;
        this.Description = description;
        this.Completed = completed;
        this.CurAmount = curAmount;
        this.ReqAmount = reqAmount;
    }

    public override void Init()
    {
        base.Init();
        EventHandler.OnEnemyDeath += EnemyDied;
    }

    void EnemyDied(IEnemy enemy)
    {
        if (enemy.ID == this.EnemyID)
        {
            //Debug.Log("Enemy kill detected: " + EnemyID);
            this.CurAmount++;
            Evaluate();
        }
    }

}
