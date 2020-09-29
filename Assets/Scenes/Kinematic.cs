using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Kinematic : MonoBehaviour
{
    public float Rotation { get; set; }
    public Vector3 Velocity { get; set; }
    public SteeringOutput Steering { get; set; }
    public GameObject targetObject = null;
    public Vector3 target;

    // Current Angle

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Velocity = Vector3.zero;
        Rotation = 0;
        Steering = new SteeringOutput();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        if (Steering == null) {
            Steering = new SteeringOutput();
            Velocity = Vector3.zero;
        }


        if (targetObject != null)
        {
            target = targetObject.transform.position;
        }

        gameObject.transform.Translate(Velocity * Time.deltaTime);

        float cAngle = gameObject.transform.GetChild(0).rotation.eulerAngles.y;
        gameObject.transform.GetChild(0).rotation = Quaternion.Euler(0, NewOrientation(cAngle, Velocity), 0);
        //gameObject.transform.Rotate(Vector3.up, Rotation * Time.deltaTime);

        Velocity += Steering.Linear * Time.deltaTime;
        //Rotation += Steering.Angular * Time.deltaTime;
    }

    protected virtual SteeringOutput GetSteering() {
        return new SteeringOutput();
    }
    
    protected float NewOrientation(float current, Vector3 velocity)
    {
        if (velocity.sqrMagnitude > 0)
        {
            return (float)(Math.Atan2(velocity.x, velocity.z) * 180 / Math.PI);
        }
        return current;
    }
}