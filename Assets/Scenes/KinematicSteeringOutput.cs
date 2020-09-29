using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicSteeringOutput
{
    public Vector3 Velocity { get; set; }
    public float Rotation { get; set; }
    
    public KinematicSteeringOutput() {
        Velocity = Vector3.zero;
        Rotation = 0;
    }
}
