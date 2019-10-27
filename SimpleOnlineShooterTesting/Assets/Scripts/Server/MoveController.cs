using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MoveController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private NetworkServerController network;
    [SerializeField]
    private float speed;
    private List<PlayerControlStatus> controlStatuses;

    private void FixedUpdate()
    {
        if (network.NotNullPlayer())
        {
            foreach (var item in network.GetPlayers())
            {
                MovePlayer(item.Value, Time.fixedDeltaTime);
            }
        }
    }

    public void MovePlayer(MyPlayer player, float interval)
    {
        Vector3 velosity = player.controlStatus == null ? Vector3.zero : 
            new Vector3(player.controlStatus.Horizontal, 0, player.controlStatus.Vertical);

        Debug.Log("speed: " + velosity * interval * speed);
        Debug.Log("OWNER: "+ PhotonNetwork.LocalPlayer.ActorNumber);
        player.transform.Translate(velosity * interval * speed);
    }
}
