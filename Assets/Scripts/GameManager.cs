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
		if (RoomManager.instance.roomType  == eRoomType.Joined)
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

		PlayerController.instance.Init();

		gameState = eGameState.Play;
	}
	void ModifyPlay() 
	{
		Debug.Log("Playing");
		if (PlayerController.instance != null)
		{
			if (PlayerController.instance.playerType == ePlayerType.Alive) 
			{
				PlayerController.instance.Move();
			}
			else if (PlayerController.instance.playerType == ePlayerType.Dead) 
			{
				InGameOver();
				return;
			}
		}
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
