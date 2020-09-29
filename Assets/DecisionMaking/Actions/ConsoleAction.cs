using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class ConsoleAction : Action
{

    public string Text { get; set; }

    public ConsoleAction(GameObject gameObject, params object[] args) : base(gameObject)
    {
        Text = (string)args[0];
    }

    public override void Execute()
    {
        Debug.Log(Text);
    }
}