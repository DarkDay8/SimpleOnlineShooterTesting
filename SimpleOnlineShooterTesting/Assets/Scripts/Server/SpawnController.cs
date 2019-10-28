using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class SpawnController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject playerPref;
    [SerializeField]
    private List<Transform> spawnPosition;
    [SerializeField]
    private float respawnTime;

    public delegate WeaponBox GetWeaponBox();
    public GetWeaponBox getWeapon;
    public delegate void FreeWeapon(string id);
    public FreeWeapon freeWeapon;
    public delegate void UpdateWeaponBoxMessage();
    public UpdateWeaponBoxMessage updateWeaponBoxMessage;

    public MyPlayer InstantiatePlayer(string id)
    {
        MyPlayer myPlayer = new MyPlayer();
        myPlayer.id = id;
        myPlayer.reSpawnPlayer = ReSpawnPlayer;  
        return Spawn(myPlayer);
    }

    private void ReSpawnPlayer(MyPlayer player)
    {
        freeWeapon(player.id);
        StartCoroutine(ReSpawn(player, respawnTime));
    }

    private IEnumerator ReSpawn(MyPlayer player, float delay)
    {
        yield return new WaitForSeconds(delay);
        Spawn(player);
        SendReSpawnMessage(player);
    }

    MyPlayer Spawn(MyPlayer player)
    {
        GameObject pl = PhotonNetwork.Instantiate(playerPref.name,
            spawnPosition[Random.Range(0, spawnPosition.Count - 1)].position,
            Quaternion.identity, 0);
        pl.AddComponent<PlayerLifeController>().SetPlayer(player);
        player.playerStatus = pl.GetComponent<PlayerStatus>();
        player.transform = pl.transform;
        player.playerStatus.SetUsedId(player.id);

        WeaponBox wb = getWeapon();
        wb.playerId = player.id;
        wb.isBusy = true;
        player.playerStatus.Weapon =  wb.weapon;
        player.playerStatus.WeaponName = wb.weapon.Title;
        updateWeaponBoxMessage();
        wb.UseMatrial();
        return player;
    }
    void SendReSpawnMessage(MyPlayer player)
    {
        object[] content = new object[] { player.id };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        SendOptions sendOptions = new SendOptions { Reliability = true, };
        PhotonNetwork.RaiseEvent((byte)GameEvent.ReSpawn, content, raiseEventOptions, sendOptions);
    }
    void Update()
    {
        
    }
}
