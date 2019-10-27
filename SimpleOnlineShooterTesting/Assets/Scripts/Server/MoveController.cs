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
        if (player.controlStatus != null)
        {
            Vector3 velosity = new Vector3(player.controlStatus.Horizontal, 0, player.controlStatus.Vertical);
            player.transform.Translate(velosity * interval * speed);
            player.transform.rotation = Quaternion.Euler(0, player.controlStatus.RotAroundY, 0);
        }
    }
}
