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
    [SerializeField]
    private WeaponController weaponController;

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
                idList.RemoveAt(0);
                return;
            }
        }
    }

    private void Start()
    {
        networkController.spawnPlayer = spawnController.InstantiatePlayer;
        spawnController.getWeapon = weaponController.GetRandomWeapon;
        spawnController.freeWeapon = weaponController.FreeWeapon;
        spawnController.updateWeaponBoxMessage = ChangeBoxMaterialMassage;
        weaponController.updateWeaponBoxMessage = ChangeBoxMaterialMassage;
    }

    public void ChangeBoxMaterialMassage()
    {
        Debug.Log("Seng Massage to Change box material");
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        SendOptions sendOptions = new SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent((byte)GameEvent.ChangeBoxMaterial, null, raiseEventOptions, sendOptions);
    }
}
