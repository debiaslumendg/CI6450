using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine : MonoBehaviour
{
    public State initialState;
    public State currentState;

    // Start is called before the first frame update
    void Start()
    {
        Game game = GameObject.Find("Game").GetComponent<Game>();
        MachineModel mm = game.Machines.Machines.Find(m => m.Name.Equals(gameObject.tag));

        Dictionary<string, State> states = new Dictionary<string, State>();

        mm.States.ForEach(sm =>
        {
            State s = new State();

            AddActions(s.EntryActions, sm.EntryActions);
            AddActions(s.Actions, sm.Actions);
            AddActions(s.ExitActions, sm.ExitActions);
            s.Name = sm.Name;

            states.Add(sm.Name, s);
        });

        mm.Transitions.ForEach(tm =>
        {
            Transition t = new Transition();

            t.TargetState = states[tm.To];
            t.Condition = CreateCondition(tm.Condition);
            
            states[tm.From].Transitions.Add(t);
        });

        initialState = states[mm.States[0].Name];
        currentState = initialState;
        initialState.EntryActions.ForEach(a => a.Execute());
    }

    private Condition CreateCondition(ConditionModel cm)
    {
        Condition c0, c1, c2;

        if (cm.Name == "Or" || cm.Name == "And")
        {
            c1 = CreateCondition(cm.Condition1);
            c2 = CreateCondition(cm.Condition2);
            object[] args = { c1, c2 };
            c0 = (Condition)Activator.CreateInstance(Type.GetType(cm.Name + "Condition"), args);
        }
        else
        {
            object[] args = new object[cm.Arguments.Count];
            for (int i = 0; i < cm.Arguments.Count; i++)
            {
                switch (cm.Arguments[i][0])
                {
                    case '#':
                        args[i] = GameObject.Find(cm.Arguments[i].Substring(1));
                        break;
                    case '$':
                        args[i] = gameObject;
                        break;
                    default:
                        args[i] = cm.Arguments[i];
                        break;
                }
            }
            Debug.Log(cm.Name);
            c0 = (Condition)Activator.CreateInstance(Type.GetType(cm.Name + "Condition"), args);
        }

        if (cm.Negated)
        {
            c0 = new NotCondition(c0);
        }

        return c0;
    }

    private void AddActions(List<Action> target, List<ActionModel> source)
    {
        source.ForEach(am =>
        {
            object[] args = new object[1 + am.Arguments.Count];
            args[0] = gameObject;

            for (int i = 0; i < am.Arguments.Count; i++)
            {
                args[i + 1] = am.Arguments[i];
            }

            Action a = (Action)Activator.CreateInstance(Type.GetType(am.Name + "Action"), args);
            target.Add(a);
        });
    }

    // Update is called once per frame
    void Update()
    {

        MachineUpdate().ForEach(a => a.Execute());

    }

    private List<Action> MachineUpdate()
    {

        // Assume no transition is triggered.
        Transition triggered = null;

        // Check through each transition and store the first one that triggers.
        foreach (Transition transition in currentState.Transitions)
        {
            if (transition.Triggered)
            {
                Debug.Log(transition.TargetState.Name + " WILL TRIGGER!");
                triggered = transition;
                break;
            }
        }

        // Check if we have a transition to fire.
        if (triggered != null)
        {
            // Find the target state.
            State targetState = triggered.TargetState;

            // Add the exit action of the old state, the transition action and the entry for the new state.
            List<Action> actions = currentState.ExitActions;
            actions.AddRange(triggered.Actions);
            actions.AddRange(targetState.EntryActions);

            // Complete the transition and return the action list.
            currentState = targetState;
            return actions;
        }
        else
        {
            return currentState.Actions;
        }

    }

}
