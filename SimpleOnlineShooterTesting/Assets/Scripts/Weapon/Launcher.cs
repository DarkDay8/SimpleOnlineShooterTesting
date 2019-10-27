using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : BaseWeapon
{
    private string bulletName = "Grenade";

    public Launcher(string id) : base(id)
    {
        Title = "Launcher";
        reload = 7;
    }

    public override void Fire(Vector3 position, Quaternion rotation)
    {
        PhotonNetwork.Instantiate(bulletName, position, rotation);
    }
}
