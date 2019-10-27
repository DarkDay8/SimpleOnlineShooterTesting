using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private new GameObject camera;
    [SerializeField]
    private float XMinRotation;
    [SerializeField]
    private float XMaxRotation;
    [SerializeField]
    [Range(1.0f, 10.0f)]
    private float Xsensitivity;
    [SerializeField]
    [Range(1.0f, 10.0f)]
    private float Ysensitivity;

    private float rotAroundX;
    private float rotAroundY;


    // Start is called before the first frame update
    private void Start()
    {
        rotAroundX = transform.eulerAngles.x;
        rotAroundY = transform.eulerAngles.y;
    }

    // Update is called once per frame
    private void Update()
    {
        rotAroundX += Input.GetAxis("Mouse Y") * Xsensitivity;
        rotAroundY += Input.GetAxis("Mouse X") * Ysensitivity;

        // Clamp rotation values
        rotAroundX = Mathf.Clamp(rotAroundX, XMinRotation, XMaxRotation);

        camera.transform.rotation = Quaternion.Euler(-rotAroundX, rotAroundY, 0); // rotation of Camera
    }

    public void SetCamera(PlayerStatus player)
    {
        Debug.Log("SetCamera");
        camera.transform.SetParent(player.transform);
        camera.transform.localPosition = new Vector3(0, 2, 0);
    }

    public void SetRotation(PlayerControlStatus pc)
    {
        pc.RotAroundX = rotAroundX;
        pc.RotAroundY = rotAroundY;
    }
}
