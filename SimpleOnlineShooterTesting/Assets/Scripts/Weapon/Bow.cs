using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bow : BaseWeapon
{
    private GameObject bullet;

    public Bow(string id) : base(id)
    {
        Title = "Bow";
        reload = 3;
    }

    public override void Fire(Vector3 position, Quaternion rotation)
    {
        PhotonNetwork.Instantiate("Arrow", position, rotation);
    }
}
