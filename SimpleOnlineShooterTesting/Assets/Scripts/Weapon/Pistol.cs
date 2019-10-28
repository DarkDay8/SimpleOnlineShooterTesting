using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : BaseWeapon
{
    public Pistol(string id) : base(id)
    {
        Title = "Pistol";
        reload = 4;
    }

    public override void Fire(Vector3 position, Vector3 forward, string id)
    {
        RaycastHit hit;
        if(Physics.Raycast(position, forward, out hit))
            hit.transform.GetComponent<PlayerLifeController>()?.Kill();
    }
}
