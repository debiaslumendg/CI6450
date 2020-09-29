using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class KinematicWander : Kinematic
{
    public float maxRotation = 3;
    public float maxSpeed = 2;

    private float cAngle;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        cAngle = (float)(gameObject.transform.GetChild(0).rotation.eulerAngles.y * Math.PI / 180);

        Steering = GetSteering();

        gameObject.transform.Translate(Steering.Linear * Time.deltaTime);
        gameObject.transform.GetChild(0).Rotate(0,Steering.Angular,0);

    }

    protected override SteeringOutput GetSteering()
    {
        SteeringOutput result = base.GetSteering();

        result.Linear = maxSpeed * new Vector3((float)Math.Sin(cAngle),0,(float)Math.Cos(cAngle));
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + result.Linear * 3, Color.red);
        result.Angular = RandomBinomial() * maxRotation;

        return result;
    }

    private float RandomBinomial()
    {
        return UnityEngine.Random.Range(0f,1f) - UnityEngine.Random.Range(0f,1f);
    }
}