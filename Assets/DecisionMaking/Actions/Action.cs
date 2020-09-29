using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{

    public GameObject Subject { get; set; }

    public Action(GameObject gameObject)
    {
        Subject = gameObject;
    }

    public virtual void Execute()
    {

    }
}