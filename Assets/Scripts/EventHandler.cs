using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public delegate void EnemyEvent(IEnemy enemy);
    public static event EnemyEvent OnEnemyDeath;

    public delegate void ItemEquippedEvent();
    public static event ItemEquippedEvent OnItemEquipped;

    public delegate void GiveGoldEvent(int gold);
    public static event GiveGoldEvent OnGiveGold;

    public delegate void GiveExpEvent(int exp);
    public static event GiveExpEvent OnGiveExp;

    public delegate void NewQuestCode();
    public static event NewQuestCode OnNewQuestCode;

    public delegate void ItemEventHandler(Item item);
    public static event ItemEventHandler OnItemAddedToInventory;

    public static void ItemAddedToInventory(Item item)
    {
        OnItemAddedToInventory?.Invoke(item);
    }

    public static void EnemyDied(IEnemy enemy)
    {
        OnEnemyDeath?.Invoke(enemy);
    }

    public static void ItemEquipped()
    {
        OnItemEquipped?.Invoke();
    }

    public static void GiveGold(int gold)
    {
        OnGiveGold?.Invoke(gold);
    }

    public static void GiveExp(int exp)
    {
        OnGiveExp?.Invoke(exp);
    }

    public static void GotNewQuestCode()
    {
        OnNewQuestCode?.Invoke();
    }

}
