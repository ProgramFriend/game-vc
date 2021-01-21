using System.Collections.Generic;

public interface IWeapon
{
    float AttackRate { get; set; }
    List<StatBase> Stats { get; set; }
    void PerformAttack();
    void PerformSpecialAttack();
}
