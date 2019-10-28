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
    [SerializeField]
    public float RotAroundX;
    [SerializeField]
    public float RotAroundY;
    [SerializeField]
    public float Fire1;
    [SerializeField]
    public float Fire2;
    [SerializeField]
    public Vector3 CameraPosition;
    [SerializeField]
    public Vector3 CameraForward;

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
            stream.SendNext(this.RotAroundX);
            stream.SendNext(this.RotAroundY);
            stream.SendNext(this.Fire1);
            stream.SendNext(this.Fire2);
            stream.SendNext(this.CameraPosition);
            stream.SendNext(this.CameraForward);
        }
        else
        {
            this.UserId = (string)stream.ReceiveNext();
            this.Horizontal = (float)stream.ReceiveNext();
            this.Vertical = (float)stream.ReceiveNext();
            this.RotAroundX = (float)stream.ReceiveNext();
            this.RotAroundY = (float)stream.ReceiveNext();
            this.Fire1 = (float)stream.ReceiveNext();
            this.Fire2 = (float)stream.ReceiveNext();
            this.CameraPosition = (Vector3)stream.ReceiveNext();
            this.CameraForward = (Vector3)stream.ReceiveNext();
        }
    }

}
