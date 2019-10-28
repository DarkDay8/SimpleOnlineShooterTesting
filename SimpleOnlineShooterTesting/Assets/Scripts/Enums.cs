using System.Collections.Generic;
using UnityEngine;

public enum  GameEvent : byte
{
    Moving = 1,
    Fire,
    Died,
    ChangeWeapon,
    InstPlayer,
    InstControl,
    ReSpawn,
    ChangeBoxMaterial
}

public enum WeaponMaterial : byte
{
    Busy,
    Pistol,
    Bow,
    Launcher
}