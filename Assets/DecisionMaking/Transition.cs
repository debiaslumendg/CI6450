using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition
{
    public bool Triggered
    {
        get
        {
            return Condition.Test();
        }
    }
    public State TargetState { get; set; }
    public List<Action> Actions { get; set; }
    public Condition Condition { get; set; }

    public Transition()
    {
        Actions = new List<Action>();
        TargetState = null;
        Condition = new Condition();
    }
}