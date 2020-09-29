using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public GameObject Player { get; set; }
    public Graph Graph { get; set; }
    public GameObjectGraphState GraphState { get; set; }
    public bool Holding { get; set; }

    private Node from;
    private Node to;
    private List<Node> wholePath;
    private float lastPathDraw = 0;
    private int lastPathDrawIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        Holding = false;
        Player = GameObject.Find("Player");
        Graph = GameObject.Find("Game").GetComponent<Graph>();
        GraphState = GameObject.Find("Player").GetComponent<GameObjectGraphState>();

        Kinematic kinematic = Player.GetComponent<Kinematic>();
        kinematic.target = Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Holding)
        // {
        //     Kinematic kinematic = Player.GetComponent<Kinematic>();

        //     Vector3 v3 = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //     v3.Set(v3.x, 0, v3.z);

        //     kinematic.target = v3;
        // }

        if (Time.realtimeSinceStartup - lastPathDraw > 0.025 && wholePath != null && lastPathDrawIndex < wholePath.Count)
        {
            lastPathDraw = Time.realtimeSinceStartup;
            Node current = wholePath[lastPathDrawIndex++];
            Debug.DrawLine(current.Center, new Vector3(current.Center.x + 2, 10, current.Center.z + 2), Color.green, 10);
            Debug.DrawLine(current.Center, new Vector3(current.Center.x - 2, 10, current.Center.z + 2), Color.green, 10);
            Debug.DrawLine(current.Center, new Vector3(current.Center.x + 2, 10, current.Center.z - 2), Color.green, 10);
            Debug.DrawLine(current.Center, new Vector3(current.Center.x - 2, 10, current.Center.z - 2), Color.green, 10);
        }

    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector3 t = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            t.Set(t.x, 0, t.z);

            Node target;
            target = Graph.NodeIn(t);
            if (target == null) return;

            List<Node> path = Graph.AStar(GraphState.Current, target);
        
            List<Vector3> vPath = new List<Vector3>();
            foreach (Node node in path)
            {
                vPath.Insert(vPath.Count, node.Center);
            }
            vPath.Insert(vPath.Count, t);

            GraphState.Path = vPath;

            if (GraphState.Path != null)
            {
                for (int i = 0; i < GraphState.Path.Count - 1; i++)
                {
                    Debug.DrawLine(GraphState.Path[i],GraphState.Path[i+1],Color.red,20);
                }
            }
        }
        
        
        
        if (context.started)
        {
            Holding = true;
        }
        else if (context.canceled)
        {
            Holding = false;
        }
    }

    public void SelectStart(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector3 v3 = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            v3.Set(v3.x, 0, v3.z);

            from = Graph.NodeIn(v3);
            if (from == null) return;
            Debug.DrawLine(from.Center, new Vector3(from.Center.x + 1, 10, from.Center.z + 1), Color.red, 20);
        }
    }

    public void SelectEnd(InputAction.CallbackContext context)
    {
        if (context.started)
        {

            Vector3 v3 = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            v3.Set(v3.x, 0, v3.z);

            to = Graph.NodeIn(v3);
            if (to == null) return;
            Debug.DrawLine(to.Center, new Vector3(to.Center.x + 1, 10, to.Center.z + 1), Color.green, 20);
        }
    }

    public void BeginPathfinding(InputAction.CallbackContext context)
    {
        if (context.started && from != null && to != null)
        {
            List<Node> path = Graph.AStar(from, to);

            wholePath = Graph.AStarWhole(from, to);

            lastPathDraw = 0;
            lastPathDrawIndex = 0;

            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(path[i].Center,path[i+1].Center,Color.yellow,20);
                }
            }

        }
    }

}
