using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHealth : MonoBehaviour, IConsumable
{
    public int HealthBonus;
    public bool MaxHealth;
    public void Consume()
    {
        Debug.Log("PotionHealth, Consume");
    }
    public void Consume(Player player)
    {
        if(MaxHealth)
            player.AddMaxHealth(HealthBonus);
        else
            player.AddHealth(HealthBonus);
    }
}
