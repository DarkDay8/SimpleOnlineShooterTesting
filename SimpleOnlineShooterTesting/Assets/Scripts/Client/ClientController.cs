using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Events;
using Photon.Realtime;
using ExitGames.Client.Photon;

[System.Serializable]
public class PlayerStatusEvent : UnityEvent<PlayerControlStatus> { }

public class ClientController : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField]
    private PlayerControlStatus playerControlPref;
    [SerializeField]
    private ButtonController buttonController;
    [SerializeField]
    private NetworkClientController networkController;
    [SerializeField]
    private CameraController cameraController;

    private PlayerStatus playerStatus;
    private PlayerControlStatus playerControl;
    private PlayerStatusEvent updateEvent = new PlayerStatusEvent();

    public void OnEvent(EventData photonEvent)
    {
        Debug.Log("Event" + photonEvent.Code);
        byte code = photonEvent.Code;
        switch (code)
        {
            case (byte)GameEvent.InstPlayer:
                object[] data = (object[])photonEvent.CustomData;
                if (((string)data[0]).Equals(PhotonNetwork.LocalPlayer.UserId))
                    Invoke("BaseInst", 0.1f);
                break;
            default:
                break;
        }
    }
    private void BaseInst()
    {
        PlayerStatus[] players = GameObject.FindObjectsOfType<PlayerStatus>();
        Debug.Log(players.Length); 
        for (int i = 0; i < players.Length; i++)
        {      
            if (players[i].IsMine(PhotonNetwork.LocalPlayer.UserId))
            {
                playerStatus = players[i];
                playerControl = PhotonNetwork.Instantiate(playerControlPref.name, Vector3.zero, Quaternion.identity, 0).GetComponent<PlayerControlStatus>();
                playerControl.SetUsedId(PhotonNetwork.LocalPlayer.UserId);
                SendAnswer();
                cameraController.SetCamera(players[i]);
                updateEvent.AddListener(buttonController.SetMoveAxes);
                updateEvent.AddListener(cameraController.SetRotation);
                return;
            }
        }
    }
    private void SendAnswer()
    {
        byte evCode = (byte)GameEvent.InstControl; 
        object[] content = new object[] { PhotonNetwork.LocalPlayer.UserId}; 
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.MasterClient };
        SendOptions sendOptions = new SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(evCode, content, raiseEventOptions, sendOptions);
    }
    private void Update()
    {
        updateEvent.Invoke(playerControl);
    }

}
