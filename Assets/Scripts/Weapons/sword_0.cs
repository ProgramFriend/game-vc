using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword_0 : MonoBehaviour, IWeapon
{
    [SerializeField]public Animator weaponAnimator;
    Animator MCAnimator { get; set; }
    PlayerMovement MCMovement { get; set; }
    public List<StatBase> Stats { get; set; }
    CharacterStats characterStats;

    public float AttackRate { get; set; }
    void Start()
    {
        AttackRate = 4f;
        MCMovement = transform.Find("/MC").GetComponent<PlayerMovement>();
        MCAnimator = transform.Find("/MC").GetComponent<Animator>();
        //characterStats = GetComponent<Player>().characterStats;

        //weaponAnimator = GetComponent<Animator>();

        //mcAnimator = GameObject.Find("/MC").GetComponent<Animator>();
    }
    public void FixedUpdate()
    {
        //weaponAnimator = this.gameObject.GetComponent<Animator>();
        if (MCMovement.movement.x > 0)
        {
            weaponAnimator.SetInteger("UD", 0);
            weaponAnimator.SetInteger("RL", 1);
        }
        else if (MCMovement.movement.x < 0)
        {
            weaponAnimator.SetInteger("UD", 0);
            weaponAnimator.SetInteger("RL", -1);
        }
        else if (MCMovement.movement.y > 0)
        {
            weaponAnimator.SetInteger("UD", 1);
            weaponAnimator.SetInteger("RL", 0);
        }
        else if (MCMovement.movement.y < 0)
        {
            weaponAnimator.SetInteger("UD", -1);
            weaponAnimator.SetInteger("RL", 0);
        }
    }
    public void PerformAttack()
    {
        //Do animation:
        //Get movement value from PlayerMovement script
        weaponAnimator.SetTrigger("Atk");
        //Debug.Log(Stats[0].GetStatValue());
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.GetComponent<IEnemy>().TakeDamage(characterStats.GetStat(StatBase.StatBaseType.Strength).GetStatValue());
        }
    }

    public void PerformSpecialAttack()
    {
        Debug.Log("Performing special attack");
    }
}
