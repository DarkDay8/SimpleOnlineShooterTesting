using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerStatus : MonoBehaviourPun, IPunObservable
{

    [SerializeField]
    string UserId;
    [SerializeField]
    float Hp;
    [SerializeField]
    public BaseWeapon Weapon;
    [SerializeField]
    public float Reload { set; get; }
    [SerializeField]
    public string WeaponName;
    public void SetUsedId(string id)
    {
        if(PhotonNetwork.IsMasterClient)
            UserId = id;
    }
    //public void SetHorizontal(string id, float value)
    //{
    //   // if (UserId.Equals(id))
    //        Horizontal = value;
    //}
    //public void SetVertical(string id, float value)
    //{
    //   // if (UserId.Equals(id))
    //        Vertical = value;
    //}


    public bool IsMine(string id)
    {
        return UserId.Equals(id);
    }
    //public void SetBodyPosition(float horizontal, float vertical)
    //{
    //    if (UserId == PhotonNetwork.LocalPlayer.UserId)
    //    {
    //        Horizontal = horizontal;
    //        Vertical = vertical;
    //    }
    //}
    //public float GetHorizontal()
    //{
    //    //return PhotonNetwork.IsMasterClient ? Horizontal : 0;
    //    return Horizontal;
    //}
    //public float GetVertical()
    //{
    //    //return PhotonNetwork.IsMasterClient ? Vertical : 0;
    //    return Vertical;
    //}
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(this.UserId);
            stream.SendNext(this.Hp);
            stream.SendNext(this.Reload);
            stream.SendNext(this.WeaponName);

        }
        else
        {
            // Network player, receive data
            this.UserId = (string)stream.ReceiveNext();
            this.Hp = (float)stream.ReceiveNext();
            this.Reload = (float)stream.ReceiveNext();
            this.WeaponName = (string)stream.ReceiveNext();
        }
    }

}
