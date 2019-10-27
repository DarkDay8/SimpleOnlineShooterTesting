using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviourPun
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float timeToLife;
    private Rigidbody rb;

    public void Start()
    {
        Debug.Log(transform.position);
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.TransformDirection(Vector3.forward * speed);
        //rb.AddForce(Vector3.forward * speed);
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
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision ArrowDestroy");
        PhotonNetwork.Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("Trigger ArrowDestroy");
        PhotonNetwork.Destroy(this.gameObject);
    }
}
