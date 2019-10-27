using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject playerPref;
    [SerializeField]
    private List<Transform> spawnPosition;

    public MyPlayer InstantiatePlayer(string id)
    {
        MyPlayer myPlayer = new MyPlayer();
        GameObject pl = PhotonNetwork.Instantiate(playerPref.name, 
            spawnPosition[Random.Range(0, spawnPosition.Count - 1)].position, 
            Quaternion.identity, 0);
        myPlayer.playerStatus = pl.GetComponent<PlayerStatus>();
        myPlayer.transform = pl.transform;
        myPlayer.playerStatus.SetUsedId(id);
        myPlayer.playerStatus.Weapon = new Launcher("Launcher1");
        return myPlayer;
    }

    public void ReSpawnPlayer(MyPlayer player)
    {
        GameObject pl = PhotonNetwork.Instantiate(playerPref.name,
            spawnPosition[Random.Range(0, spawnPosition.Count - 1)].position,
            Quaternion.identity, 0);
        player.playerStatus = pl.GetComponent<PlayerStatus>();
        player.transform = pl.transform;
        player.playerStatus.Weapon = new Launcher("Launcher1");
    }

    void Update()
    {
        
    }
}
