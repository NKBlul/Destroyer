using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eGameState : uint
{
	None,
	Menu,
	Play,
	GameOver
}


public class GameManager : MonoBehaviour
{
	public eGameState gameState = eGameState.None;
	void Start() 
	{
		InMenu();
	}

	#region FSM Menu
	void InMenu() 
	{
		Debug.Log("InMenu");
		gameState = eGameState.Menu;
	}
	void ModifyMenu() 
	{
		InPlay();
		return;
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
		InGameOver();
		return;
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
			case eGameState.Menu:
				ModifyMenu();
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
