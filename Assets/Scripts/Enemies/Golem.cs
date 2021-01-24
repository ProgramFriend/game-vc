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

	private Player player;
	public Spawner spawner { get; set; }

	[System.Serializable]
	public class EnemyStats
	{
		public int giveDamage;
		public int maxHealth;

		public int giveGoldMax;
		public int giveGoldMin;
		[HideInInspector] public int giveGold { get; set; }

		private int _curHealth;
		public int curHealth
		{
			get { return _curHealth; }
			set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
		}

		public void Init()
		{
			curHealth = maxHealth;
			giveGold = Random.Range(giveGoldMin, giveGoldMax);
		}
	}
	public EnemyStats stats = new EnemyStats();

	private void Awake()
	{
		stats.Init();
		statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
		ID = 2;
		Experience = 35;

		characterStats = new CharacterStats(6, 2, 2);

		player = GameObject.Find("MC").GetComponent<Player>();
	}

	public void TakeDamage(int damage)
	{
		stats.curHealth -= damage;
		statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
		if (stats.curHealth <= 0)
		{
			Die();
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
		player.DamagePlayer(characterStats.GetStat(StatBase.StatBaseType.Strength).GetStatValue());
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			PerformAttack();
		}
	}
}
