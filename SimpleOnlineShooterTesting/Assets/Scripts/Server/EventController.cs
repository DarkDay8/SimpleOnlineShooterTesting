using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class ServerController : MonoBehaviourPunCallbacks, IOnEventCallback
{

    public void OnEvent(EventData eventData)
    {
        Debug.Log("Sender: " + eventData.Sender);
        byte code = eventData.Code;
        switch (code)
        {
            case (byte)GameEvent.InstControl:
                Debug.Log("InstControl");
                object[] data = (object[])eventData.CustomData;
                //MoveController.Instanse.MovePlayer(eventData., (float)data[0], (float)data[1], (float)data[2]);
                break;
            default:
                break;
        }
    }

}
