using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition
{
    private Game game = GameObject.Find("Game").GetComponent<Game>();
    public virtual bool Test(params object[] args) {
        return true;
    }
}