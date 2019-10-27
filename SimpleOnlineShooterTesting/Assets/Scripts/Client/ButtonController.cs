using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class ButtonController : MonoBehaviourPunCallbacks
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void SetMoveAxes(PlayerControlStatus pc )
    {
        string id = PhotonNetwork.LocalPlayer.UserId;
        pc.Horizontal = Input.GetAxis("Horizontal");
        pc.Vertical = Input.GetAxis("Vertical");
    }
}
