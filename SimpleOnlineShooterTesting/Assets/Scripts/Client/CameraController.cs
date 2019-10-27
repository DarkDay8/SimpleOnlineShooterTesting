using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject camera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCamera(PlayerStatus player)
    {
        Debug.Log("SetCamera");
        camera.transform.SetParent(player.transform);
        camera.transform.localPosition = new Vector3(0, 2, 0);
    }
}
