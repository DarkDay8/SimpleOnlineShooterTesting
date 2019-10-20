using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkServerController : MonoBehaviourPunCallbacks
{
    private Dictionary<int, GameObject> playerListEntries;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server!");
        RoomOptions options = new RoomOptions { MaxPlayers = 3 };

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
            playerListEntries = new Dictionary<int, GameObject>();
        }
    }

    public override void OnPlayerEnteredRoom(Player player)
    {
        Debug.Log("Player entered in the room:" + player.ActorNumber);
        playerListEntries.Add(player.ActorNumber, SwawnController.Instance.InstantiatePlayer());
    }

    public override void OnPlayerLeftRoom(Player player)
    {
        Debug.Log("Player leaved the room:" + player.ActorNumber);
        GameObject destoyplayer;
        playerListEntries.TryGetValue(player.ActorNumber, out destoyplayer);
        SwawnController.Instance.DestroyPlayer(destoyplayer);
        playerListEntries.Remove(player.ActorNumber);

    }

}
