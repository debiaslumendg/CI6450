using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachinesModel
{
    public List<MachineModel> Machines { get; set; }

}

public class MachineModel
{
    public string Name { get; set; }
    public List<StateModel> States { get; set; }
    public List<TransitionModel> Transitions { get; set; }
}

public class StateModel
{
    public string Name { get; set; }
    public List<ActionModel> EntryActions { get; set; }
    public List<ActionModel> Actions { get; set; }
    public List<ActionModel> ExitActions { get; set; }
}

public class ActionModel
{
    public string Name { get; set; }
    public List<string> Arguments { get; set; }
}

public class TransitionModel
{
    public string From { get; set; }
    public string To { get; set; }
    public ConditionModel Condition { get; set; }
}

public class ConditionModel
{
    public bool Negated { get; set; }
    public string Name { get; set; }
    public List<string> Arguments { get; set; }
    public ConditionModel Condition1 { get; set; }
    public ConditionModel Condition2 { get; set; }
}
