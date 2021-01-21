using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats
{
    public List<StatBase> stats = new List<StatBase>();

    public CharacterStats(int strength, int toughness, int attackSpeed)
    {
        stats = new List<StatBase>()
        {
            new StatBase(StatBase.StatBaseType.Strength, strength, "Strength"),
            new StatBase(StatBase.StatBaseType.Toughness, toughness, "Toughness"),
            new StatBase(StatBase.StatBaseType.AttackSpeed, attackSpeed, "AttackSpeed")
        };
    }

    public StatBase GetStat(StatBase.StatBaseType statType_)
    {
        return this.stats.Find(x => x.StatType == statType_);
    }

    public void AddStatBonus(List<StatBase> statBases)
    {
        foreach(StatBase statBase in statBases)
        {
            GetStat(statBase.StatType).AddStatBonus(new StatBonus(statBase.BaseValue));
        }
    }

    public void RemoveStatBonus(List<StatBase> statBases)
    {
        foreach(StatBase statBase in statBases)
        {
            GetStat(statBase.StatType).RemoveStatBonus(new StatBonus(statBase.BaseValue));
        }
    }
}
