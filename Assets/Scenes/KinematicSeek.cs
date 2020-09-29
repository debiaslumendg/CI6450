using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class KinematicSeek : Kinematic
{
    public Boolean flee;
    public float maxSpeed = 2;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        Steering = GetSteering();

        gameObject.transform.Translate(Steering.Linear * Time.deltaTime);
        if (Steering.Linear.sqrMagnitude > 0)
        {
            gameObject.transform.GetChild(0).rotation = Quaternion.Euler(0, NewOrientation(0, Steering.Linear), 0);
        }
    }

    protected override SteeringOutput GetSteering()
    {
        SteeringOutput result = base.GetSteering();

        result.Linear = target - gameObject.transform.position;
        if (flee) result.Linear *= -1;
        result.Linear = result.Linear.normalized * maxSpeed;

        result.Linear.Set(result.Linear.x, 0, result.Linear.z);

        result.Angular = 0;

        return result;
    }

}