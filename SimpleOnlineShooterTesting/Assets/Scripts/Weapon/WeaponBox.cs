using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponBox : MonoBehaviourPun, IPunObservable
{
    public List<Material> weaponBoxMaterial;
    public BaseWeapon weapon;
    public WeaponMaterial material;
    public string playerId;
    public bool isBusy = false;

    public void SetWeapon(BaseWeapon weapon, WeaponMaterial material)
    {
        this.weapon = weapon;
        this.material = material;
        UseMatrial();
    }

    public void UseMatrial()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (isBusy)
            renderer.material = weaponBoxMaterial[(int)WeaponMaterial.Busy];
        else
            renderer.material = weaponBoxMaterial[(int)material];
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext((byte)this.material);
            stream.SendNext(this.isBusy);


        }
        else
        {
            // Network player, receive data
            this.material = (WeaponMaterial)(byte)stream.ReceiveNext();
            this.isBusy = (bool)stream.ReceiveNext();

        }
    }
}
