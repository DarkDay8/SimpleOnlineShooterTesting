using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public static MoveController Instanse;

    [SerializeField]
    private NetworkServerController network;
    [SerializeField]
    private float speed;

    private void Awake()
    {
        Instanse = this;
    }

    public void MovePlayer(int sender, PlayerAxes playerAxes)
    {
        Vector3 velosity = new Vector3(playerAxes.Horizontal, playerAxes.Vertical, 0);
        GameObject player = network.getPlayer(sender);
        player.transform.Translate(velosity * speed);
    }
    public void MovePlayer(int sender, float horizontal, float vertical, float interval)
    {
        Debug.Log("horizontal " + horizontal + " vertical " + vertical + "interval " + interval);
        Vector3 velosity = new Vector3(horizontal, 0, vertical);
        GameObject player = network.getPlayer(sender);
        player.transform.Translate(velosity * interval * speed);
    }
}
