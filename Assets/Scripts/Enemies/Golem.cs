using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Golem : MonoBehaviour, IEnemy
{
	private CharacterStats characterStats;

	[SerializeField] private StatusIndicator statusIndicator;

	public int ID { get; set; }
	public int Experience { get; set; }
	public Spawner spawner { get; set; }

	private Player player;
	private Animator anim;
	private float difference;

	public Transform specAttkPoint;
	public GameObject specialSword;
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
		Experience = 100;

		characterStats = new CharacterStats(stats.giveDamage, 2, 2);

		player = GameObject.Find("MC").GetComponent<Player>();
		anim = GetComponent<Animator>();
	}

    private void Update()
    {
		difference = Vector2.Distance(this.transform.position, player.transform.position);
        if(difference < 10f && difference > 5f)
        {
			//PerformSpecialAttack();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			InvokeRepeating(nameof(PerformAttack), 0f, 0.8f);
		}
	}
    private void OnTriggerExit2D(Collider2D col)
    {
		if (col.gameObject.CompareTag("Player"))
		{
			StopAttack();
		}
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
			Invoke(nameof(Die), 3.9f);
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
		Invoke(nameof(DoDamage), 0.4f);
		anim.SetTrigger("Attack");
	}
	private void DoDamage()
	{
		player.DamagePlayer(characterStats.GetStat(StatBase.StatBaseType.Strength).GetStatValue());
	}
	public void PerformSpecialAttack()
    {
		anim.SetTrigger("SpecialAttack");
		GameObject swords = Instantiate(specialSword, specAttkPoint.position, specAttkPoint.rotation, this.transform);

		Destroy(swords, 1f);
	}
	private void StopAttack()
    {
		CancelInvoke();
		anim.SetBool("isMoving", true);
	}
}
