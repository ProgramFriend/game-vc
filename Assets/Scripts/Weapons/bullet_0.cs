using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_0 : MonoBehaviour
{
    public int damage;
    public GameObject hitEffect;

    public List<StatBase> Stats { get; set; }
    //CharacterStats characterStats;

    public float AttackRate { get; set; }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Enemy") && !col.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<IEnemy>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
