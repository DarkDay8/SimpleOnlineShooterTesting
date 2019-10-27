using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviourPun
{
    [SerializeField]
    private NetworkServerController network;
    [SerializeField]
    private float weaponUp;

    private void FixedUpdate()
    {
        if (network.NotNullPlayer())
        {
            foreach (var item in network.GetPlayers())
            {
                if (item.Value.playerStatus.Reload > 0)
                    item.Value.playerStatus.Reload -= Time.fixedDeltaTime;
                else if (item.Value.controlStatus?.Fire1 > 0)
                    Fire(item.Value);
            }
        }
    }
    public void Fire(MyPlayer player)
    {
        player.playerStatus.Weapon.Fire(
            player.playerStatus.transform.position + Vector3.up * weaponUp + player.playerStatus.transform.TransformDirection(Vector3.forward),//TransformDirection(Vector3.forward * 2),
            Quaternion.Euler(-player.controlStatus.RotAroundX, player.controlStatus.RotAroundY, 0));
        player.playerStatus.Reload = player.playerStatus.Weapon.reload;
    }
}
 