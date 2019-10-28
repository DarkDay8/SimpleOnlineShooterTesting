using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponController : MonoBehaviourPun
{
    public delegate void UpdateWeaponBoxMessage();
    public UpdateWeaponBoxMessage updateWeaponBoxMessage;
    [SerializeField]
    private NetworkServerController network;
    [SerializeField]
    private List<WeaponBox> weapons;

    private void Start()
    {
        weapons[0].SetWeapon(new Pistol("Pistol1"), WeaponMaterial.Pistol);
        weapons[1].SetWeapon(new Pistol("Pistol2"),WeaponMaterial.Pistol);
        weapons[2].SetWeapon(new Bow("Bow1"), WeaponMaterial.Bow);
        weapons[3].SetWeapon(new Bow("Bow2"), WeaponMaterial.Bow);
        weapons[4].SetWeapon(new Launcher("Launcher1"),WeaponMaterial.Launcher);
        weapons[5].SetWeapon(new Launcher("Launcher2"), WeaponMaterial.Launcher);
    }

    public void FreeWeapon(string id)
    {
        foreach (WeaponBox weapon in weapons)
        {
            if (weapon.playerId == id)
            {
                weapon.playerId = "";
                weapon.isBusy = false;
                updateWeaponBoxMessage();
                weapon.UseMatrial();
            }
        }
    }
    public void TakeWeapon(string id, WeaponBox newWeapon)
    {
        foreach (WeaponBox weapon in weapons)
        {
            if (weapon.weapon.Id == newWeapon.weapon.Id)
            {
                weapon.playerId = id;
                weapon.isBusy = true;
                updateWeaponBoxMessage();
                weapon.UseMatrial();
            }
        }
    }
    public  void ChangeWeapon(string id, WeaponBox newWeapon)
    {
        FreeWeapon(id);
        TakeWeapon(id, newWeapon);
       
    }

    public WeaponBox GetRandomWeapon()
    {
        WeaponBox weapon;
        do
        {
            weapon = weapons[Random.Range(0, weapons.Count - 1)];
        }
        while (weapon.isBusy);
        return weapon;
    }

    private void FixedUpdate()
    {
        if (network.NotNullPlayer())
        {
            foreach (var item in network.GetPlayers())
            {
                if (item.Value.playerStatus?.Reload > 0)
                    item.Value.playerStatus.Reload -= Time.fixedDeltaTime;
                else if (item.Value.controlStatus?.Fire1 > 0)
                    Fire(item.Value);
                if (item.Value.controlStatus?.Fire2 > 0)
                {
                    Change(item.Value);
                }
            }
        }
    }
    private void Change(MyPlayer player)
    {
        float range = 1f;
        RaycastHit[] hits = Physics.SphereCastAll(player.controlStatus.CameraPosition, range, player.controlStatus.CameraForward, range);
        Debug.Log("hits " + hits);
        foreach (var item in hits)
        {
            WeaponBox wb = item.transform.GetComponent<WeaponBox>();
            Debug.Log("wb " + wb);
            if (wb != null)
            {
                ChangeWeapon(player.id, wb);
                player.playerStatus.Weapon = wb.weapon;
                return;
            }
        }

        //RaycastHit hit;
        //if (Physics.Raycast(player.controlStatus.CameraPosition, player.controlStatus.CameraForward, out hit, range))
        //{
        //    WeaponBox wb = hit.transform.GetComponent<WeaponBox>();
        //    if (wb != null)
        //    {
        //        ChangeWeapon(player.id, wb);
        //        player.playerStatus.Weapon = wb.weapon;
        //    }
        //}
    }

    private void Fire(MyPlayer player)
    {
        player.playerStatus.Reload = player.playerStatus.Weapon.reload;
        player.playerStatus.Weapon.Fire(player.controlStatus.CameraPosition,
            player.controlStatus.CameraForward, player.id);
    }
}
 