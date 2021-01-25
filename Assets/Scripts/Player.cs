using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterStats characterStats;
	public GameObject deathUI;

	private PlayerMovement playerMovement;

	int maxHealth;

    public int RespawnTime { get; set; }
    public int TotalDeaths { get; set; }
    public int TotalKills { get; set; }
    private int _health;
	public int curHealth
    {
        get
        {
			return _health;
        }
        set
        {
			_health = Mathf.Clamp(value, 0, maxHealth);
        }
    }
	public int gold { get; set; }

	private float regenHPWaitTime = 2f;
	private float regenHPWait;

	[SerializeField]
	private StatusIndicatorPlayer statusIndicator;

	void Start()
	{
		deathUI.SetActive(false);

		EventHandler.OnGiveGold += AddGold;

		characterStats = new CharacterStats(1, 1, 2);
		playerMovement = GetComponent<PlayerMovement>();

		maxHealth = 50;
		_health = maxHealth;
		RespawnTime = 4;

		statusIndicator.SetHealth(curHealth, maxHealth);

		AddGold(300);
	}

    private void Update()
    {
		if (curHealth < maxHealth)
		{
			if (regenHPWait <= 0)
			{
				curHealth += 1;
				regenHPWait = regenHPWaitTime;
				statusIndicator.SetHealth(curHealth);
			}
			regenHPWait -= Time.deltaTime;
			
		}
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
		if(col.CompareTag("NPC"))
        {
			col.gameObject.GetComponent<Interactable>().CallInteraction();
        }
        else if(col.CompareTag("Interactable Object"))
        {
			col.gameObject.GetComponent<Interactable>().Interact();
		}/////
		else if (col.gameObject.CompareTag("Enemy"))
		{
			col.gameObject.GetComponent<IEnemy>().TakeDamage(characterStats.GetStat(StatBase.StatBaseType.Strength).GetStatValue());
		}
		/////
	}
	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag("NPC"))
		{
			col.gameObject.GetComponent<Interactable>().CallOffInteraction();
		}
	}
	public void DamagePlayer(int damage)
	{
		curHealth -= damage;
		if (curHealth <= 0)
            PlayerDied();
		statusIndicator.SetHealth(curHealth, maxHealth);
	}

	public void AddHealth(int toAdd)
    {
		curHealth += toAdd;
		statusIndicator.SetHealth(curHealth, maxHealth);
	}

	public void AddMaxHealth(int toAdd)
    {
		maxHealth += toAdd;
		statusIndicator.SetHealth(curHealth, maxHealth);
    }

	public void AddGold(int otherGold)
    {
		gold += otherGold;
		statusIndicator.SetGold(gold);
    }

	public void MinusGold(int otherGold)
	{
		gold -= otherGold;
		statusIndicator.SetGold(gold);
	}

	public void PlayerDied()
    {
		PauseControl.PauseGame();
		deathUI.SetActive(true);
		StartCoroutine("Respawn");
		this.transform.position = Vector2.zero;
		curHealth = maxHealth;
		TotalDeaths++;
		AddGold(-(int)(gold*0.5));
    }
	IEnumerator Respawn()
	{
		yield return new WaitForSecondsRealtime(RespawnTime);
		PauseControl.PauseGame();
		deathUI.SetActive(false);
		StopCoroutine("Respawn");		
	}
}

