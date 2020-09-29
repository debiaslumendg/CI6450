using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatCondition : Condition
{

    public float MinValue { get; set; }
    public float MaxValue { get; set; }

    public FloatCondition(float min, float max) : base()
    {
        MinValue = min;
        MaxValue = max;
    }

    public override bool Test(params object[] args) {
        float testValue = (float)args[0];
        return MinValue <= testValue && testValue <= MaxValue;
    }
}