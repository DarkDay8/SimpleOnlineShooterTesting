using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class EventController : MonoBehaviourPunCallbacks, IOnEventCallback
{

    public void OnEvent(EventData eventData)
    {
        Debug.Log("Sender: " + eventData.Sender);
        byte code = eventData.Code;
        switch (code)
        {
            case (byte)GameEvent.Moving:
                Debug.Log("Moving");
                object[] data = (object[])eventData.CustomData;
                MoveController.Instanse.MovePlayer(eventData.Sender, (float)data[0], (float)data[1], (float)data[2]);
                break;
            default:
                break;
        }

    }
    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }


}
