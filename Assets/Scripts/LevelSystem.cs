using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;


public class LevelSystem : MonoBehaviour
{
    public GameObject LevelUpParticles;
    public GameObject LevelUpCanvas;

    [SerializeField] private StatusIndicatorPlayer statusIndicatorMC;
    public int Level { get; set; }
    public int CurExp { get; set; }
    //public int ReqExp { get { return 100*(Level+1)*(Level+1) - (100 * Level * Level); } }
    public int ReqExp { get { return Level * 50; } }


    void Start()
    {
        EventHandler.OnEnemyDeath += EnemyToExperience;
        EventHandler.OnGiveExp += UpdateXP;
        Level = 1;
    }

    public void EnemyToExperience(IEnemy enemy)
    {
        UpdateXP(enemy.Experience);
    }

    public void UpdateXP(int newXP)
    {
        CurExp += newXP;
        StartCoroutine(nameof(Delay), 0.5f);
        while (CurExp >= ReqExp)
        {
            LevelUp();
            break;
        }
        statusIndicatorMC.SetExp(CurExp, ReqExp, Level);
        //UIEventHandler.PlayerLevelChanged();
    }

    public void LevelUp()
    {
        CurExp -= ReqExp;
        Level++;
        statusIndicatorMC.SetExp(CurExp, ReqExp, Level);
        ShopSystem.Instance.UpdateItems();
        Instantiate(LevelUpParticles, this.transform);
        StartCoroutine(LvlUpParticles(1.2f));
    }

    public IEnumerator Delay(float timeToDelay)
    {
        yield return new WaitForSeconds(timeToDelay);
    }

    public IEnumerator LvlUpParticles(float timeToDelay)
    {
        LevelUpParticles.SetActive(true);
        LevelUpCanvas.SetActive(true);
        yield return new WaitForSeconds(timeToDelay);
        LevelUpParticles.SetActive(false);
        LevelUpCanvas.SetActive(false);
    }


}



/*
public class LevelSystem : MonoBehaviour
{
    private int xpnextlevel, curLvl, totalXP;

    public int currentLevel;
    public int differencexp, maxXP; // differencexp / totaldifference

    public void UpdateXP(int newXP)
    {
        totalXP += newXP;
        curLvl = (int)(0.1f * Mathf.Sqrt(totalXP));
        if(curLvl != currentLevel)
        {
            currentLevel = curLvl;
            //Animation to show MC reached a new level
        }

        xpnextlevel = 100 * (currentLevel + 1) * (currentLevel + 1); //Total xp for next level
        maxXP = xpnextlevel - (100 * currentLevel * currentLevel); //Amount of xp to reach next level
        differencexp = maxXP - xpnextlevel + totalXP;
    }
}*/