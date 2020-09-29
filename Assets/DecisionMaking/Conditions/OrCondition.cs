using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrCondition : Condition
{

    public Condition Condition1 { get; set; }
    public Condition Condition2 { get; set; }

    public OrCondition(Condition cond1, Condition cond2) : base()
    {
        Condition1 = cond1;
        Condition2 = cond2;
    }

    public override bool Test(params object[] args) {
        return Condition1.Test() || Condition2.Test();
    }
}