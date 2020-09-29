using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Game : MonoBehaviour
{
    public StateMachinesModel Machines { get; set; }
    public string GraphModel { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        Machines = JsonConvert.DeserializeObject<StateMachinesModel>(System.IO.File.ReadAllText(@"Assets\DecisionMaking\StateMachines.json"));
        GraphModel = System.IO.File.ReadAllText(@"Assets\Pathfinding\map.poly");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
