using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviourPun
{
    float size = 1;
    float maxSize = 6;
    float increase = 10;
    float timeOfLife = 2;
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        if (size < maxSize)
        {
            size += Time.fixedDeltaTime * increase;
            transform.localScale = new Vector3(size, size, size);
        }
        if (timeOfLife > 0)
            timeOfLife -= Time.fixedDeltaTime;
        else
            PhotonNetwork.Destroy(this.gameObject);
    }
}
