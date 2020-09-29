using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class FleeAction : Action
{

    public GameObject From { get; set; }

    public FleeAction(GameObject gameObject, params object[] args) : base(gameObject)
    {
        From = (GameObject)args[0];
    }

    public override void Execute()
    {
        Seek seek = Subject.GetComponent<Seek>();
        seek.targetObject = From;
        seek.flee = true;
        seek.enabled = true;
    }
}