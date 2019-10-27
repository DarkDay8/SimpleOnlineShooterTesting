using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerControlStatus : MonoBehaviourPun, IPunObservable
{
    [SerializeField]
    string UserId;
    [SerializeField]
    public float Horizontal;
    [SerializeField]
    public float Vertical;

    public void SetUsedId(string id)
    {
        if (photonView.IsMine)
            UserId = id;
    }
    public string GetUsedId()
    {
        return UserId;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.UserId);
            stream.SendNext(this.Horizontal);
            stream.SendNext(this.Vertical);
        }
        else
        {
            this.UserId = (string)stream.ReceiveNext();
            this.Horizontal = (float)stream.ReceiveNext();
            this.Vertical = (float)stream.ReceiveNext();
        }
    }

}
