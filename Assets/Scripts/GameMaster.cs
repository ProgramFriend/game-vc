using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
	public LevelSystem levelSystem;

	public static void KillPlayer(Player player)
	{
		Destroy(player.gameObject);
	}
}
