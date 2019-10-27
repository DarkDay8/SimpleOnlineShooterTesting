using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkClientController : MonoBehaviourPunCallbacks
{
    public int ActorNumber { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        ActorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }



    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("don't join random room " + message);
    }
}
