using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviourPun
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float timeToLife;
    private Rigidbody rb;
    private string explosionName = "Explosion";

    public void Start()
    {
        Debug.Log(transform.position);
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.TransformDirection(Vector3.forward * speed);
    }
    private void FixedUpdate()
    {
        if (timeToLife > 0)
            timeToLife -= Time.fixedDeltaTime;
        else
            Detonation();
    }

    private void OnTriggerEnter(Collider other)
    {
        Detonation();
    }

    private void Detonation()
    {
        PhotonNetwork.Instantiate(explosionName, transform.position, Quaternion.identity);
        PhotonNetwork.Destroy(this.gameObject);
    }
}
