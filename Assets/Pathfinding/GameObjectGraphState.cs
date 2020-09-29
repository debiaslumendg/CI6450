using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectGraphState : MonoBehaviour
{
    private Graph graph;
    public Node Current { get; set; }
    public List<Vector3> Path { get; set; }

    private bool arrivedNext = true;

    // Start is called before the first frame update
    void Start()
    {
        graph = GameObject.Find("Game").GetComponent<Graph>();
        Path = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Current == null) Current = graph.NodeIn(gameObject.transform.position);

        // Constantly updates current Node
        if (!Current.PointInTriangle(gameObject.transform.position))
        {
            bool found = false;

            for (int i = 0; !found && i < Current.Neighbors.Count; i++)
            {
                if (found = Current.Neighbors[i].PointInTriangle(gameObject.transform.position))
                {
                    Current = Current.Neighbors[i];
                }
                for (int j = 0; !found && j < Current.Neighbors[i].Neighbors.Count; j++)
                {

                    if (found = Current.Neighbors[i].Neighbors[j].PointInTriangle(gameObject.transform.position))
                    {
                        Current = Current.Neighbors[i].Neighbors[j];
                    }

                }
            }

        }

        if (Path.Count > 1)
        {
            if (arrivedNext)
            {
                gameObject.GetComponent<Kinematic>().target = new Vector3((Path[0].x + Path[1].x) / 2, 0, (Path[0].z + Path[1].z) / 2);
                arrivedNext = false;
            }
        }
        else if (Path.Count == 1)
        {
            if (arrivedNext)
            {
                gameObject.GetComponent<Kinematic>().target = new Vector3(Path[0].x, 0, Path[0].z);
                arrivedNext = false;
            }
        }

        if (Path.Count > 0 && (gameObject.transform.position - gameObject.GetComponent<Kinematic>().target).magnitude < 0.6)
        {
            arrivedNext = true;
            Path.RemoveAt(0);
        }

    }
}
