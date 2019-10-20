using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SwawnController : MonoBehaviourPunCallbacks
{
    static public SwawnController Instance;

    [SerializeField]
    private GameObject playerPref;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject InstantiatePlayer()
    {
        return PhotonNetwork.Instantiate(playerPref.name, new Vector3(0f, 2f, 0f), Quaternion.identity, 0);
    }

    public void DestroyPlayer(GameObject player)
    {
        PhotonNetwork.Destroy(player);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
