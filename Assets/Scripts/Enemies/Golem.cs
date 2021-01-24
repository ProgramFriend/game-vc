using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Golem : MonoBehaviour, IEnemy
{
	private CharacterStats characterStats;


	/// <summary>
	/// 
	/// </summary>
	//[Header("Optional: ")]

	[SerializeField] private StatusIndicator statusIndicator;

	public int ID { get; set; }
	public int Experience { get; set; }
	public Spawner spawner { get; set; }

	private Player player;
	private Animator anim;

	[System.Serializable]
	public class EnemyStats
	{
		public int giveDamage;
		public int maxHealth;
		public int giveGold;

		private int _curHealth;
		public int curHealth
		{
			get { return _curHealth; }
			set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
		}

		public void Init()
		{
			curHealth = maxHealth;
			giveGold = 100;
		}
	}
	public EnemyStats stats = new EnemyStats();

	private void Awake()
	{
		stats.Init();
		statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
		ID = 100;
		Experience = 35;

		characterStats = new CharacterStats(stats.giveDamage, 2, 2);

		player = GameObject.Find("MC").GetComponent<Player>();
		anim = GetComponent<Animator>();
	}

	public void TakeDamage(int damage)
	{
		stats.curHealth -= damage;
		statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
		if (stats.curHealth <= 0)
		{
			GetComponent<GolemFollow>().enabled = false;
			CancelInvoke();
			anim.Play("Death");
			Invoke("Die", 4);
		}
	}

	public void Die()
	{
		EventHandler.EnemyDied(this);
		EventHandler.GiveGold(stats.giveGold);
		player.TotalKills++;
		Destroy(this.gameObject);
	}

	public void PerformAttack()
	{
		anim.SetBool("isMoving", false);
		Invoke("DoDamage", 0.4f);
		anim.SetTrigger("Attack");
	}

	private void DoDamage()
    {
		player.DamagePlayer(characterStats.GetStat(StatBase.StatBaseType.Strength).GetStatValue());
	}
	private void StopAttack()
    {
		CancelInvoke();
		anim.SetBool("isMoving", true);
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			InvokeRepeating("PerformAttack", 0f, 0.8f);
			
		}
	}
    private void OnTriggerExit2D(Collider2D col)
    {
		if (col.gameObject.CompareTag("Player"))
		{
			StopAttack();
		}
	}
}
