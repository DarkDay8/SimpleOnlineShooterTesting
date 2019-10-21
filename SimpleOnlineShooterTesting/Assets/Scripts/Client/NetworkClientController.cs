using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkClientController : MonoBehaviourPunCallbacks
{
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
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("don't join random room " + message);
    }
}
