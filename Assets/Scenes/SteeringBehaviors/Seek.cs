using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Seek : Kinematic
{
    public Boolean flee;
    public float maxAcceleration = 10;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        Steering = GetSteering();

        base.Update();
    }

    protected override SteeringOutput GetSteering()
    {
        SteeringOutput result = base.GetSteering();

        result.Linear = target - gameObject.transform.position;
        if (flee) result.Linear *= -1;
        result.Linear = result.Linear.normalized * maxAcceleration;
        result.Linear.Set(result.Linear.x, 0, result.Linear.z);

        result.Angular = 0;

        return result;
    }

}