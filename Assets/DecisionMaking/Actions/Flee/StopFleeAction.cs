using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class StopFleeAction : Action
{
    public StopFleeAction(GameObject gameObject, params object[] args) : base(gameObject)
    {
    }

    public override void Execute()
    {
        Subject.GetComponent<Seek>().enabled = false;
    }
}