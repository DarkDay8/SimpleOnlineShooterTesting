using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon 
{

    public string Id { get; set; }
    public  float reload { get; set; }
    public string Title { get; set; }
    public BaseWeapon(string id)
    {
        this.Id = id;
    }
    public abstract void Fire(Vector3 position, Quaternion rotation);
}
