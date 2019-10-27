using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class MyServerController : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField]
    private MoveController moveController;
    [SerializeField]
    private SpawnController spawnController;
    [SerializeField]
    private NetworkServerController networkController;

    private List<string> idList = new List<string>();

    public void OnEvent(EventData eventData)
    {
        Debug.Log("Sender: " + eventData.Sender);
        byte code = eventData.Code;
        switch (code)
        {
            case (byte)GameEvent.InstControl:
                Debug.Log("InstControl");
                object[] data = (object[])eventData.CustomData;
                idList.Add((string)data[0]);
                Invoke("MyPlayerModify", 0.1f);
                break;
            default:
                break;
        }
    }
    IEnumerator Delay(string id, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Debug.Log("id = " + id);
        //MyPlayerModify(id);
    }
    private void MyPlayerModify()
    {
        string id = idList[0];
        PlayerControlStatus[] controls = GameObject.FindObjectsOfType<PlayerControlStatus>();
        for (int i = 0; i < controls.Length; i++)
        {
            if (controls[i].GetUsedId().Equals(id))
            {
                networkController.GetPlayer(id).controlStatus = controls[i];
                //MyPlayer player = networkController.GetPlayer(id);
                //controls[i].transform.SetParent(player.playerStatus.transform);
                //controls[i].transform.localPosition = new Vector3(0, 2, 0);
                //player.controlStatus = controls[i];
                idList.RemoveAt(0);
                return;
            }
        }
    }

    private void Awake()
    {
        networkController.spawnPlayer = spawnController.InstantiatePlayer;
    }

}
