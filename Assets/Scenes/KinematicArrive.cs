using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class KinematicArrive : KinematicSeek
{
    public float radius = 5f;
    public float timeToTarget = 0.25f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override SteeringOutput GetSteering()
    {
        SteeringOutput result = base.GetSteering();

        result.Angular = 0;

        result.Linear = target - gameObject.transform.position;
        if (result.Linear.magnitude < radius) 
        {
            result.Linear = Vector3.zero;
            return result;
        }

        result.Linear /= timeToTarget;

        if (result.Linear.magnitude > maxSpeed)
        {
            result.Linear = result.Linear.normalized * maxSpeed;
            result.Linear.Set(result.Linear.x, 0, result.Linear.z);
        }

        return result;
    }
}