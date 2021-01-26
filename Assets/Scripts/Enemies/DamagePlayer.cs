using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
	private Player player;
	public int DoDamage;

	void Start()
    {
		player = GameObject.Find("MC").GetComponent<Player>();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			Invoke(nameof(PerformAttack), 0.2f);
		}
	}
	private void PerformAttack()
    {
		player.DamagePlayer(DoDamage);
	}
}
