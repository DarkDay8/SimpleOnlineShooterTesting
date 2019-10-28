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
        reload = 2;
    }

    public override void Fire(Vector3 position, Vector3 forward, string id)
    {
        GameObject buller = PhotonNetwork.Instantiate(bulletName, position, Quaternion.identity);
        buller.GetComponent<Grenade>()?.Fly(forward, id);
    }
}
