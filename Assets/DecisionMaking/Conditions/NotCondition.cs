using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotCondition : Condition
{

    public Condition Condition { get; set; }

    public NotCondition(Condition cond) : base()
    {
        Condition = cond;
    }

    public override bool Test(params object[] args) {
        return !Condition.Test();
    }
}