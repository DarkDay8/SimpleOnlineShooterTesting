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
    [SerializeField]
    private GUIController guiController;

    private PlayerStatus playerStatus;
    private PlayerControlStatus playerControl;
    private PlayerStatusEvent updateEvent = new PlayerStatusEvent();
    private List<WeaponBox> weaponBoxes;

    private void Start()
    {
        if (weaponBoxes == null)
            weaponBoxes = new List<WeaponBox>(FindObjectsOfType<WeaponBox>());
    }

    public void OnEvent(EventData photonEvent)
    {
        try
        {
            object[] data = (object[])photonEvent.CustomData;
            byte code = photonEvent.Code;
            switch (code)
            {
                case (byte)GameEvent.InstPlayer:
                    if (((string)data[0]).Equals(PhotonNetwork.LocalPlayer.UserId))
                        Invoke("BaseInst", 0.1f);
                    break;
                case (byte)GameEvent.ReSpawn:
                    if (((string)data[0]).Equals(PhotonNetwork.LocalPlayer.UserId))
                        Invoke("ReSpawn", 0.1f);
                    break;
                case (byte)GameEvent.ChangeBoxMaterial:
                    Invoke("ChangeBoxMaterial", 0.1f);
                    break;
                default:
                    break;
            }
        }
        catch (System.Exception)
        {
            Debug.Log("Data " + photonEvent.CustomData);
            Debug.Log("Code " + photonEvent.Code);
        }
    }
    private void ReSpawn()
    {
        PlayerStatus[] players = GameObject.FindObjectsOfType<PlayerStatus>();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].IsMine(PhotonNetwork.LocalPlayer.UserId))
            {
                cameraController.SetCamera(players[i]);
                guiController.SetPlayerStatus(players[i]);
                return;
            }
        }
    }
    private void ChangeBoxMaterial()
    {
        foreach (var item in weaponBoxes)
            item.UseMatrial();
    }
    private void BaseInst()
    {
        PlayerStatus[] players = GameObject.FindObjectsOfType<PlayerStatus>();
        //Debug.Log(players.Length); 
        for (int i = 0; i < players.Length; i++)
        {      
            if (players[i].IsMine(PhotonNetwork.LocalPlayer.UserId))
            {
                playerStatus = players[i];
                playerControl = PhotonNetwork.Instantiate(playerControlPref.name, Vector3.zero, Quaternion.identity, 0).GetComponent<PlayerControlStatus>();
                playerControl.SetUsedId(PhotonNetwork.LocalPlayer.UserId);
                SendAnswer();
                cameraController.SetCamera(players[i]);
                guiController.SetPlayerStatus(players[i]);
                updateEvent.AddListener(buttonController.SetMoveAxes);
                updateEvent.AddListener(buttonController.SetAtherAxes);
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
