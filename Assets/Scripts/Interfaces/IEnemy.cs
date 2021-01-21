using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    Spawner spawner { get; set; }
    int ID { get; set; }
    int Experience { get; set; }
    void Die();
    void TakeDamage(int amount);
    void PerformAttack();
}
