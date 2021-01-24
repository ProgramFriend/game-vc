using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Slayer : MonoBehaviour, IEnemy
{
	//[Header("Optional: ")]

	[SerializeField] private StatusIndicator statusIndicator;

	public int ID { get; set; }
	public int Experience { get; set; }

	public LevelSystem levelSystem;
	public Player player;
	public Transform playerTransform;
	public Spawner spawner { get; set; }
	public float FollowDistance = 5f;
	[System.Serializable]
	public class EnemyStats
	{
		public int giveDamage;
		public int maxHealth;

		public int giveGoldMax;
		public int giveGoldMin;
		[HideInInspector] public int giveGold;

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
		if (statusIndicator == null)
		{
			Debug.LogError("No status indicator referenced on Player");
		}
		else
		{
			statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
		}
		ID = 1;
		Experience = 10;


	}

	private void Update()
	{
		if (Vector2.Distance(this.transform.position, playerTransform.position) <= FollowDistance)
		{
			gameObject.GetComponent<EnemyPatrol>().enabled = false;
			gameObject.GetComponent<EnemyAI>().enabled = true;
		}
		else
		{
			gameObject.GetComponent<EnemyPatrol>().enabled = true;
			gameObject.GetComponent<EnemyAI>().enabled = false;
		}
	}

	public void TakeDamage(int damage)
	{
		stats.curHealth -= damage;
		statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
		if (stats.curHealth <= 0)
		{
			Die();
			Destroy(this.gameObject);
		}
	}

	public void Die()
	{
		EventHandler.EnemyDied(this);

		player.AddGold(stats.giveGold);
	}

	public void PerformAttack()
	{
		player.DamagePlayer(stats.giveDamage);
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			PerformAttack();
		}
	}
}
