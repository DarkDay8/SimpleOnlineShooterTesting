using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeController : MonoBehaviourPun
{
    private MyPlayer player;
    public void SetPlayer(MyPlayer player)
    {
        this.player = player;
    }

    private void OnTriggerEnter(Collider other)
    {
        player.reSpawnPlayer(player);
        PhotonNetwork.Destroy(this.gameObject);
    }
}
