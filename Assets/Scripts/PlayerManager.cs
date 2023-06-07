using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
	#region singletone
	public static PlayerManager instance;
	public void Awake()
	{
		instance = this;
	}
	#endregion

	public List<Player> players = new List<Player>();

	// Called when a player connects to the server
	public void AddPlayer(GameObject playerObj)
	{
		Player player  = playerObj.GetComponent<Player>();

		// player object enabled
		playerObj.SetActive(true);
		// Initialize player
		player.Init();
		// add player into the players list
		players.Add(player);
	}

	// Called when a player disconnects from the server
	public void RemovePlayer(GameObject playerObj)
	{
		Player player = playerObj.GetComponent<Player>();

		// player object disabled
		playerObj.SetActive(false);
		// remove player from the players list
		players.Remove(player);
	}

	public void MovePlayers()
	{
		for (int i = 0; i < players.Count; i++)
		{
			players[i].Move();
		}
	}
}
