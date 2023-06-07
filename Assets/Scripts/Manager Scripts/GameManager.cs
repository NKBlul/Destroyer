using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum eGameState : uint
{
	None,
	Init,
	Play,
	GameOver
}


public class GameManager : MonoBehaviour
{
	public eGameState gameState = eGameState.None;
	void Start() 
	{
		Init();
	}

	#region FSM Init
	void Init() 
	{
		Debug.Log("Init");
		RoomManager.instance.Init();

		gameState = eGameState.Init;
	}
	void ModifyInit() 
	{
		if (PlayerManager.instance.players.Count > 0)
		{
			InPlay();
			return;
		}
	}
	#endregion

	#region FSM Play
	void InPlay() 
	{
		Debug.Log("InPlay");

		gameState = eGameState.Play;
	}
	void ModifyPlay() 
	{
		Debug.Log("Playing");

		if (PlayerManager.instance.players.Count <= 0)
		{
			InGameOver();
			return;
		}
		PlayerManager.instance.MovePlayers();
	}
    #endregion

    #region FSM GameOver
    void InGameOver() 
	{
		Debug.Log("InGameOver");
		gameState = eGameState.GameOver;
	}
	void ModifyGameOver() 
	{
		return;
	}
	#endregion

	void Update() 
	{
		switch (gameState) 
		{
			case eGameState.Init:
				ModifyInit();
				break;
			case eGameState.Play:
				ModifyPlay();
				break;
			case eGameState.GameOver:
				ModifyGameOver();
				break;
		}
	}
}
