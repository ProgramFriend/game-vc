using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
	public CharacterStats characterStats;


	/// <summary>
	/// 
	/// </summary>
	//[Header("Optional: ")]

	[SerializeField] private StatusIndicator statusIndicator;

	public GameObject DeathParticles;
	public int ID { get; set; }
    public int Experience { get; set; }

	private Player player;
	private Transform playerTransform;
	public Spawner spawner { get; set; }
	public float FollowDistance = 5f;
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
		if (statusIndicator == null)
		{
			Debug.LogError("No status indicator referenced on Player");
        }
        else 
		{
			statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
		}
		ID = 0;
		Experience = 35;

		characterStats = new CharacterStats(6, 2, 2);
	}
    private void Start()
    {
		player = GameObject.Find("MC").GetComponent<Player>();
		playerTransform = player.transform;
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
		}
	}

	public void Die()
    {
		Instantiate(DeathParticles, new Vector3(transform.position.x, transform.position.y, -3), Quaternion.identity, this.transform.parent);
		EventHandler.EnemyDied(this);
		EventHandler.GiveGold(stats.giveGold);
		player.TotalKills++;
		this.spawner.Respawn();
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
