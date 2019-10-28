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
        pc.Horizontal = Input.GetAxis("Horizontal");
        pc.Vertical = Input.GetAxis("Vertical");
    }
    public void SetAtherAxes(PlayerControlStatus pc)
    {
        pc.Fire1 = Input.GetAxis("Fire1");
        pc.Fire2 = Input.GetAxis("Fire2");
    }
}
