using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class MyPlayer
{
    public Transform transform;
    public PlayerStatus playerStatus;
    public PlayerControlStatus controlStatus;
}

public class NetworkServerController : MonoBehaviourPunCallbacks
{
    public delegate MyPlayer SpawnPlayer(string id);
    public SpawnPlayer spawnPlayer;
    private Dictionary<string, MyPlayer> playerListEntries;

    //public delegate 

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }


    void Update()
    {

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server!");
        RoomOptions options = new RoomOptions { MaxPlayers = 3, PublishUserId = true };

        PhotonNetwork.CreateRoom("TestRoom", options, null);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room was been created");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined to the room");
        if (playerListEntries == null)
        {
            playerListEntries = new Dictionary<string, MyPlayer>();
        }
    }

    public override void OnPlayerEnteredRoom(Player player)
    {
        Debug.Log("Player entered in the room:" + player.UserId);
        playerListEntries.Add(player.UserId, spawnPlayer(player.UserId));
        object[] content = new object[] { player.UserId };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        SendOptions sendOptions = new SendOptions { Reliability = true, };
        PhotonNetwork.RaiseEvent((byte)GameEvent.InstPlayer, content, raiseEventOptions, sendOptions);
    }

    public override void OnPlayerLeftRoom(Player player)
    {
        Debug.Log("Player leaved the room:" + player.ActorNumber);
        MyPlayer destoyplayer;
        playerListEntries.TryGetValue(player.UserId, out destoyplayer);
        if (destoyplayer.controlStatus != null)
            PhotonNetwork.Destroy(destoyplayer.controlStatus.gameObject);
        if (destoyplayer.playerStatus != null)
            PhotonNetwork.Destroy(destoyplayer.playerStatus.gameObject);
        playerListEntries.Remove(player.UserId);

    }
    public MyPlayer GetPlayer(string id)
    {
        MyPlayer player;
        playerListEntries.TryGetValue(id, out player);
        return player;
    }

    public Dictionary<string, MyPlayer> GetPlayers()
    {
        return playerListEntries;
    }
    public bool NotNullPlayer()
    {
        if (playerListEntries != null)
            return playerListEntries.Count > 0;
        else return false;
    }
}
