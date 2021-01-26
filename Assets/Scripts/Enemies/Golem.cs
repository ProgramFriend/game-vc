using System.Collections.Generic;
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
	Vector3 center;
	Vector3 spawnPos;
	private GameObject[] swords = new GameObject[12];


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

	private float waitTime = 5f;
	private float someTime;

	private GolemFollow golemFollow;
	private bool isDying;

	private void Awake()
	{
		stats.Init();
		statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
		ID = 100;
		Experience = 100;

		characterStats = new CharacterStats(stats.giveDamage, 2, 2);

		player = GameObject.Find("MC").GetComponent<Player>();
		anim = GetComponent<Animator>();
		golemFollow = GetComponent<GolemFollow>();
	}

    private void Update()
    {
		center = this.transform.position;


		difference = Vector2.Distance(this.transform.position, player.transform.position);
        if(!isDying && difference < 5.5f && difference > 3.5f)
        {
			if (someTime <= 0)
			{
				PerformSpecialAttack();
				someTime = waitTime;
			}
        }
		someTime -= Time.deltaTime;
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
			CancelInvoke(nameof(PerformAttack));
			isDying = true;
			golemFollow.enabled = false;
			GetComponent<BoxCollider2D>().enabled = false;
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
		player.DamagePlayer(characterStats.GetStat(StatBase.StatBaseType.Strength).GetStatValue());
		anim.SetTrigger("Attack");
	}

	public void PerformSpecialAttack()
    {
		anim.SetTrigger("SpecialAttack");
		golemFollow.enabled = false;
		for (int i = 0; i < 12; i++)
		{
			spawnPos = TheCircle(center, 4f, i);
			swords[i] = (Instantiate(specialSword, spawnPos, Quaternion.identity));
		}
		for(int i=0; i<12; i++)
        {
			Destroy(swords[i], 2f);
			Invoke(nameof(StopAttack), 1.9f);
        }
	}
	private void StopAttack()
    {
		anim.SetBool("isMoving", true);
		if(!isDying) golemFollow.enabled = true;
		CancelInvoke(nameof(PerformAttack));
	}

	Vector3 TheCircle(Vector3 center, float radius, int index)
	{
		float ang = index * 30;
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
		pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		pos.z = center.z;
		return pos;
	}
}
