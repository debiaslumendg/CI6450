using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public string Name { get; set; }
    public List<Action> Actions { get; set; }
    public List<Action> EntryActions { get; set; }
    public List<Action> ExitActions { get; set; }
    public List<Transition> Transitions { get; set; }

    public State () {
        Actions = new List<Action>();
        EntryActions = new List<Action>();
        ExitActions = new List<Action>();
        Transitions = new List<Transition>();
    }
}