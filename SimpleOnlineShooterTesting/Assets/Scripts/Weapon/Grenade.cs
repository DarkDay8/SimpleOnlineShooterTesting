using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviourPun, IBullet
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float timeToLife;
    private Rigidbody rb;
    private string explosionName = "Explosion";
    private string id;

    public void Start()
    {
        Debug.Log("Grenate ID inst : " + id);
        rb = GetComponent<Rigidbody>();
        //rb.velocity = transform.TransformDirection(Vector3.forward * speed);
    }
    private void FixedUpdate()
    {
        if (timeToLife > 0)
            timeToLife -= Time.fixedDeltaTime;
        else
            Detonation();
    }

    public void Fly(Vector3 forward, string id)
    {
        Debug.Log("Grenate Fly");
        this.id = id;
        GetComponent<Rigidbody>().velocity = transform.TransformDirection(forward * speed);
    }

    public void SetId(string id)
    {
        this.id = id;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger " + other.transform.GetComponent<PlayerLifeController>());
        if (id == other.transform.GetComponent<PlayerLifeController>()?.GetId())
            return;
        Detonation();
    }

    private void Detonation()
    {
        PhotonNetwork.Instantiate(explosionName, transform.position, Quaternion.identity);
        PhotonNetwork.Destroy(this.gameObject);
    }

    public string GetId()
    {
        return id;
    }
}
