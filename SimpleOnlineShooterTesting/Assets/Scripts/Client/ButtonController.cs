using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class ButtonController : MonoBehaviourPunCallbacks
{
    RaiseEventOptions raiseEventOptions;
    SendOptions sendOptions;
    [SerializeField]
    float moveButtonInterval = 0.1f;
    float timer = 0;


    void Start()
    {
        raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.MasterClient };
        sendOptions = new SendOptions { Reliability = false,  };
    }

    void Update()
    {
        if (timer > moveButtonInterval)
        {
            SendAxes(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), timer);
            timer = 0;
        }
        else
            timer += Time.deltaTime;
    }

    void SendAxes(float horizontal, float vertical, float interval)
    {
        //PlayerAxes playerAxes = new PlayerAxes(horizontal, vertical);
        object[] content = new object[] { horizontal, vertical, interval };
        PhotonNetwork.RaiseEvent((byte)GameEvent.Moving, content, raiseEventOptions, sendOptions);
    }
}
