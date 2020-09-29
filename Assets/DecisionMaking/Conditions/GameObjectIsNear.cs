using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectIsNearCondition : Condition
{
    public GameObject Self { get; set; }
    public GameObject TargetObject { get; set; }
    public float Radius { get; set; }

    public GameObjectIsNearCondition(GameObject self, GameObject targetObject, string radius) : base()
    {
        Self = self;
        TargetObject = targetObject;
        Radius = float.Parse(radius);
    }

    public override bool Test(params object[] args) {
        return (Self.transform.position - TargetObject.transform.position).magnitude <= Radius;
    }
}