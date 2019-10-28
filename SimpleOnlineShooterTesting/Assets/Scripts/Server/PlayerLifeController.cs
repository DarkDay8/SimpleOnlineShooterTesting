using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeController : MonoBehaviourPun
{
    private MyPlayer player;
    private string id;
    public void SetPlayer(MyPlayer player)
    {
        this.player = player;
        id = player.id;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<IBullet>()?.GetId() == id)
            return;
        Kill();
    }
    public void Kill()
    {
        player.reSpawnPlayer(player);
        PhotonNetwork.Destroy(this.gameObject);
    }
    public string GetId()
    {
        return id;
    }
}
