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
    [SerializeField]
    [Range(1.0f, 10.0f)]
    private float CameraUp;
    private float rotAroundX;
    private float rotAroundY;
    Transform playerTransform;


    // Start is called before the first frame update
    private void Start()
    {
        Cursor.visible = false;
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

        camera.transform.rotation = Quaternion.Euler(-rotAroundX, rotAroundY, 0);
        if (playerTransform != null)
            camera.transform.position = playerTransform.position + Vector3.up * CameraUp;
    }

    public void SetCamera(PlayerStatus player)
    {
        playerTransform = player.transform;
        camera.transform.localPosition = new Vector3(0, 2, 0);
    }

    public void SetRotation(PlayerControlStatus pc)
    {
        pc.RotAroundX = rotAroundX;
        pc.RotAroundY = rotAroundY;
        pc.CameraPosition = camera.transform.position;
        pc.CameraForward = camera.transform.forward;
    }
}
