using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class StatBase
{
    public enum StatBaseType { Strength, Toughness, AttackSpeed }

    public List<StatBonus> StatBonusAdditives { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public StatBaseType StatType { get; set; }
    public int BaseValue { get; set; }
    public string StatName { get; set; }
    public string StatDescription { get; set; }
    public int FinalValue { get; set; }

    [JsonConstructor]
    public StatBase(StatBaseType statType, int baseValue, string statName)
    {
        this.StatBonusAdditives = new List<StatBonus>();
        this.StatType = statType;
        this.BaseValue = baseValue;
        this.StatName = statName;
    }


    public void AddStatBonus(StatBonus statBonus)
    {
        StatBonusAdditives.Add(statBonus);
    }

    public void RemoveStatBonus(StatBonus statBonus)
    {
        StatBonusAdditives.Remove(StatBonusAdditives.Find(x=> x.BonusValue == statBonus.BonusValue));
    }

    public int GetStatValue()
    {
        this.FinalValue = 0;
        this.StatBonusAdditives.ForEach(x => this.FinalValue += x.BonusValue);
        this.FinalValue += BaseValue;
        return FinalValue;
    }


}
