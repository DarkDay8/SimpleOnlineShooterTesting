using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviourPun, IBullet
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float timeToLife;
    private Rigidbody rb;
    private string id;

    public void Start()
    {
        Debug.Log(transform.position);
        rb = GetComponent<Rigidbody>();
        //rb.velocity = transform.TransformDirection(Vector3.forward * speed);
    }
    private void FixedUpdate()
    {
        if (rb.velocity != Vector3.zero)
            rb.rotation = Quaternion.LookRotation(rb.velocity);
        if (timeToLife > 0)
            timeToLife -= Time.fixedDeltaTime;
        else
            PhotonNetwork.Destroy(this.gameObject);
    }
    public void Fly(Vector3 forward, string id)
    {
        this.id = id;
        GetComponent<Rigidbody>().velocity = transform.TransformDirection(forward * speed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision ArrowDestroy");
        PhotonNetwork.Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (id == other.transform.GetComponent<PlayerLifeController>()?.GetId())
            return;
        PhotonNetwork.Destroy(this.gameObject);
    }

    public string GetId()
    {
        return id;
    }
}
