using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Align : Kinematic
{
    public float maxAngularAcceleration = 10;
    public float maxSpeed = 50;
    public float targetRadius = 2;
    public float slowRadius = 5;
    public float timeTotarget = 0.1f;
    

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

    // protected override SteeringOutput GetSteering()
    // {
    //     // SteeringOutput result = base.GetSteering();

    //     //  = target - gameObject.transform.position;

    //     // float distance = result.Linear.magnitude;
    //     // if (distance < targetRadius) return null;


    //     // float targetSpeed = 0;
    //     // if (distance > slowRadius) 
    //     //     targetSpeed = maxSpeed;
    //     // else
    //     //     targetSpeed = maxSpeed * distance / slowRadius;

    //     // result.Linear = result.Linear.normalized * maxAcceleration;
    //     // result.Linear.Set(result.Linear.x, 0, result.Linear.z);

    //     // Vector3 targetVelocity = result.Linear.normalized * targetSpeed;

    //     // result.Linear = (targetVelocity - Velocity) / timeTotarget;

    //     // if (result.Linear.magnitude > maxAcceleration)
    //     //     result.Linear = result.Linear.normalized * maxAcceleration;

    //     // result.Angular = 0;

    //     // return result;
    // }

}