using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class SayAction : Action
{

    public string Text { get; set; }

    public SayAction(GameObject gameObject, params object[] args) : base(gameObject)
    {
        Text = (string)args[0];
    }

    public override void Execute()
    {
        Subject.transform.Find("Bubble").GetComponent<TextMeshPro>().text = Text;
    }
}