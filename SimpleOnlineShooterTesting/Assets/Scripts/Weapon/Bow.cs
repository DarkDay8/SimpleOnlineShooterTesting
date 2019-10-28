using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bow : BaseWeapon
{
    private string  bulletName = "Arrow";

    public Bow(string id) : base(id)
    {
        Title = "Bow";
        reload = 1;
    }

    public override void Fire(Vector3 position, Vector3 forvard, string id)
    {
        GameObject buller = PhotonNetwork.Instantiate(bulletName, position, Quaternion.identity);
        buller.GetComponent<Arrow>()?.Fly(forvard, id);
    }
}
