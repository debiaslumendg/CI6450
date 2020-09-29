using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseIsNearCondition : Condition
{
    public GameObject Self { get; set; }
    public float Radius { get; set; }

    public MouseIsNearCondition(GameObject self, string radius) : base()
    {
        Self = self;
        Radius = float.Parse(radius);
    }

    public override bool Test(params object[] args) {
        Vector3 mp = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mp = new Vector3(mp.x,0,mp.z);
        return (Self.transform.position - mp).magnitude <= Radius;
    }
}