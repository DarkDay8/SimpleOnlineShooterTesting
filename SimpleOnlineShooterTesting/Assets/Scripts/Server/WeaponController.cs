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
                if (item.Value.playerStatus?.Reload > 0)
                    item.Value.playerStatus.Reload -= Time.fixedDeltaTime;
                else if (item.Value.controlStatus?.Fire1 > 0)
                    Fire(item.Value);
            }
        }
    }
    public void Fire(MyPlayer player)
    {
        player.playerStatus.Reload = player.playerStatus.Weapon.reload;
        player.playerStatus.Weapon.Fire(player.controlStatus.CameraPosition, player.controlStatus.CameraForward, player.id);
//            player.playerStatus.transform.position + Vector3.up * weaponUp + player.playerStatus.transform.TransformDirection(Vector3.forward),
//           Quaternion.Euler(-player.controlStatus.RotAroundX, player.controlStatus.RotAroundY, 0));
        
    }
}
 