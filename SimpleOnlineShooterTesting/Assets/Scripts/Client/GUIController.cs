using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    [SerializeField]
    private Image reload;
    [SerializeField]
    private Text Title;

    private Color red = Color.red;
    private Color green = Color.green;

    private PlayerStatus player;
    public void SetPlayerStatus(PlayerStatus player)
    {
        this.player = player;
    }
   
    void Update()
    {
        if (player != null)
        {
            reload.color = player.Reload > 0 ? red : green;
            Title.text = player.WeaponName;
        }
    }
}
