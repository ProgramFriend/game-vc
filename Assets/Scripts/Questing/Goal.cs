using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal
{
    public Quest Quest { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }
    public int CurAmount { get; set; }
    public int ReqAmount { get; set; }

    public virtual void Init()
    {
        //default init stuff
    }

    public void Evaluate()
    {
        if (CurAmount >= ReqAmount)
        {
            Complete();
        }
    }

    public void Complete()
    {
        Quest.CheckGoals();
        Completed = true;
    }

}
