using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public enum eRoomType
{
	None,
    Joined,
    Left
}

public class RoomManager : MonoBehaviourPunCallbacks
{
	#region singletone
	public static RoomManager instance;
    private void Awake() 
    {
        if (instance != null) 
        {
            Destroy(gameObject);
            return;
        } 
        else 
        {
            instance = this;
        }
    }
	#endregion
    public eRoomType roomType = eRoomType.None;

	public GameObject player;

    [Space]
    public Transform spawnPoint;

    public void Init() 
    {
        Connecting();
    }

    public void Connecting() 
    {
		Debug.Log("Connecting...");

		PhotonNetwork.ConnectUsingSettings();
	}

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Connected to Server");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        PhotonNetwork.JoinOrCreateRoom("test", null, null);       
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        Debug.Log("We're connected and in a room");

        GameObject Player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
        Player.GetComponent<PlayerSetup>().IsLocalPlayer();

        roomType = eRoomType.Joined;
	}

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
    }
}
