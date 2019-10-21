using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject camera;
    bool isSet;

    // Start is called before the first frame update
    void Start()
    {
        isSet = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSet)
        {
            PlayerStatus[] players = GameObject.FindObjectsOfType<PlayerStatus>();
            foreach (PlayerStatus item in players)
            {
                if(item.IsMine(PhotonNetwork.LocalPlayer.ActorNumber))
                {
                    camera.transform.SetParent(item.transform);
                    camera.transform.localPosition = new Vector3(0, 2, 0);
                    isSet = true;
                }
            }
        }
    }
}
