using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringOutput
{
    public Vector3 Linear { get; set; }
    public float Angular { get; set; }
    
    public SteeringOutput() {
        Linear = Vector3.zero;
        Angular = 0;
    }
}
