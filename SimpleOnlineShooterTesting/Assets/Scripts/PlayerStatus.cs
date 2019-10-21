using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerStatus : MonoBehaviourPun, IPunObservable
{

    [SerializeField]
    public int ActorNumber;
    [SerializeField]
    public float Hp;

    void SetActor(int actor)
    {
        ActorNumber = actor;
    }

    public bool IsMine(int localActor)
    {
        return localActor == ActorNumber;
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(this.ActorNumber);
            stream.SendNext(this.Hp);
        }
        else
        {
            // Network player, receive data
            this.ActorNumber = (int)stream.ReceiveNext();
            this.Hp = (float)stream.ReceiveNext();
        }
    }

}
