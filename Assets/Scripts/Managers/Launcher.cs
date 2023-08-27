using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.LowLevel;

public class Launcher : MonoBehaviourPunCallbacks
{
	public static Launcher Instance;

	[SerializeField] TMP_InputField roomNameInputField;
	[SerializeField] TMP_Text errorText;
	[SerializeField] TMP_Text roomNameText;
	[SerializeField] Transform roomListContent;
	[SerializeField] GameObject roomListItemPrefab;
	[SerializeField] Transform playerListContent;
	[SerializeField] GameObject PlayerListItemPrefab;
	[SerializeField] GameObject startGameButton;
	[SerializeField] Image logo;

    private bool buttonPressed;
	private bool onMaster;

	void Awake()
	{
		Instance = this;
        buttonPressed = false;
        onMaster = false;
    }

	void Start()
	{
		Debug.Log("Connecting to Master");
		PhotonNetwork.ConnectUsingSettings();
	}

	public override void OnConnectedToMaster()
	{
		Debug.Log("Connected to Master");
		onMaster = true;
        PhotonNetwork.AutomaticallySyncScene = true;
	}

	private void Update()
	{
        //if (Keyboard.current.anyKey.wasPressedThisFrame && buttonPressed == false && onMaster == true)
        //{
        //	buttonPressed = true;
        //    PhotonNetwork.JoinLobby();
        //}
        if (Input.anyKeyDown && buttonPressed == false && onMaster == true)
        {
            buttonPressed = true;
            PhotonNetwork.JoinLobby();
        }
    }

	private void FadeOutLogo()
	{
		Color originalColor = logo.color;
		float elapsedTime = 0f;
		float fadeTime = 3f;

		while (elapsedTime < fadeTime)
		{
			elapsedTime += Time.deltaTime;
			float alpha = 1f - (elapsedTime/5f); //above fade time, higher alpha. below fade time, lower alpha 
			Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
			logo.color = newColor;
		}
		//Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
		//logo.color = transparentColor;
	}

    IEnumerator LogoTime()
	{
		if (Menu.Instance.menuName == "logo")
		{
			FadeOutLogo();
		}
		yield return new WaitForSeconds(1f);
        MenuManager.Instance.OpenMenu("title");
        Debug.Log("Joined Lobby");
    }

	public override void OnJoinedLobby()
	{
        StartCoroutine(LogoTime());      
    }

    public void CreateRoom()
	{
		if(string.IsNullOrEmpty(roomNameInputField.text))
		{
			return;
		}
		PhotonNetwork.CreateRoom(roomNameInputField.text);
		MenuManager.Instance.OpenMenu("loading");
	}

	public override void OnJoinedRoom()
	{
		MenuManager.Instance.OpenMenu("room");
		roomNameText.text = PhotonNetwork.CurrentRoom.Name;

		Player[] players = PhotonNetwork.PlayerList;

		foreach(Transform child in playerListContent)
		{
			Destroy(child.gameObject);
		}

		for(int i = 0; i < players.Count(); i++)
		{
			Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
		}

		startGameButton.SetActive(PhotonNetwork.IsMasterClient);
	}

	public override void OnMasterClientSwitched(Player newMasterClient)
	{
		startGameButton.SetActive(PhotonNetwork.IsMasterClient);
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		errorText.text = "Room Creation Failed: " + message;
		Debug.LogError("Room Creation Failed: " + message);
		MenuManager.Instance.OpenMenu("error");
	}

	public void StartGame()
	{
		PhotonNetwork.LoadLevel(1);
	}

	public void Win()
	{
		PhotonNetwork.LoadLevel(2);
	}

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
		MenuManager.Instance.OpenMenu("loading");
	}

	public void JoinRoom(RoomInfo info)
	{
		PhotonNetwork.JoinRoom(info.Name);
		MenuManager.Instance.OpenMenu("loading");
	}

	public override void OnLeftRoom()
	{
		MenuManager.Instance.OpenMenu("title");
	}

	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		foreach(Transform trans in roomListContent)
		{
			Destroy(trans.gameObject);
		}

		for(int i = 0; i < roomList.Count; i++)
		{
			if(roomList[i].RemovedFromList)
				continue;
			Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
		}
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
	}
}