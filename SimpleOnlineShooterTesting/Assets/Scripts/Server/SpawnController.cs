using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject playerPref;

    public GameObject InstantiatePlayer()
    {
        return PhotonNetwork.Instantiate(playerPref.name, new Vector3(0f, 2f, 0f), Quaternion.identity, 0);
    }


    void Update()
    {
        
    }
}
