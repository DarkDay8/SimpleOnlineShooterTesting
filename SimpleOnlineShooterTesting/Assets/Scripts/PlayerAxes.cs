using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAxes 
{
    public float Horizontal { get; set; }
    public float Vertical { get; set; }

    public PlayerAxes() : this(0f, 0f) { }
    public PlayerAxes(float horizontal, float vertical)
    {
        Horizontal = horizontal;
        Vertical = vertical;
    }

}
