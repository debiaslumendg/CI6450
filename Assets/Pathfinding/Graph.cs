using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Priority_Queue;

public class Graph : MonoBehaviour
{
    Dictionary<int, Node> nodes = new Dictionary<int, Node>();

    void Start()
    {
        string[] inputs = GameObject.Find("Game").GetComponent<Game>().GraphModel.Split('|');

        int pointTotal = int.Parse(inputs[0].Split(',')[0]);
        Dictionary<int, Vector3> points = new Dictionary<int, Vector3>();

        for (int i = 0; i < pointTotal; i++)
        {
            string[] point = inputs[i + 1].Split(',');
            points[int.Parse(point[0])] = new Vector3(float.Parse(point[1]) / 10, 0, -float.Parse(point[2]) / 10);
        }

        int triangleTotal = int.Parse(inputs[pointTotal + 1].Split(',')[0]);
        for (int i = 0; i < triangleTotal; i++)
        {
            string[] triangle = inputs[i + pointTotal + 2].Split(',');
            nodes[int.Parse(triangle[0])] = new Node(points[int.Parse(triangle[1])], points[int.Parse(triangle[2])], points[int.Parse(triangle[3])]);
        }

        for (int i = 1; i <= triangleTotal; i++)
        {
            for (int j = i + 1; j <= triangleTotal; j++)
            {
                if (nodes[i].IsNeighbor(nodes[j]))
                {
                    nodes[i].Neighbors.Add(nodes[j]);
                    nodes[j].Neighbors.Add(nodes[i]);
                }
            }
        }

        foreach (Node triangle in nodes.Values)
        {
            triangle.Draw(100000);
        }

    }

    public Node NodeIn(Vector3 point)
    {
        foreach (Node node in nodes.Values)
        {
            if (node.PointInTriangle(point))
            {
                return node;
            }
        }
        return null;
    }

    public float h(Node from, Node to)
    {
        return Vector3.Distance(from.Center, to.Center);
    }

    public List<Node> ReconstructPath(Dictionary<Node, Node> cameFrom, Node current)
    {
        List<Node> path = new List<Node>() { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Insert(0, current);
        }
        return path;
    }

    public List<Node> AStar(Node start, Node goal)
    {
        SimplePriorityQueue<Node> openSet = new SimplePriorityQueue<Node>();
        openSet.Enqueue(start, 0);

        Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();

        Dictionary<Node, float> gScore = new Dictionary<Node, float>();
        gScore[start] = 0;

        Dictionary<Node, float> fScore = new Dictionary<Node, float>();
        fScore[start] = h(start, goal);

        while (openSet.Count > 0)
        {
            Node current = openSet.Dequeue();

            if (current == goal) return ReconstructPath(cameFrom, current);

            foreach (Node neighbor in current.Neighbors)
            {
                float tentativeGScore = gScore[current] + Vector3.Distance(current.Center, neighbor.Center);

                if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + h(neighbor, goal);
                    if (openSet.Contains(neighbor)) openSet.Remove(neighbor);
                    openSet.Enqueue(neighbor, fScore[neighbor]);
                }
            }
        }

        return null;
    }

    public List<Node> AStarWhole(Node start, Node goal)
    {
        SimplePriorityQueue<Node> openSet = new SimplePriorityQueue<Node>();
        openSet.Enqueue(start, 0);

        Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();

        Dictionary<Node, float> gScore = new Dictionary<Node, float>();
        gScore[start] = 0;

        Dictionary<Node, float> fScore = new Dictionary<Node, float>();
        fScore[start] = h(start, goal);

        List<Node> wholePath = new List<Node>();

        while (openSet.Count > 0)
        {
            Node current = openSet.Dequeue();
            wholePath.Insert(wholePath.Count,current);

            if (current == goal) return wholePath; //ReconstructPath(cameFrom, current);

            foreach (Node neighbor in current.Neighbors)
            {
                float tentativeGScore = gScore[current] + Vector3.Distance(current.Center, neighbor.Center);

                if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + h(neighbor, goal);
                    if (openSet.Contains(neighbor)) openSet.Remove(neighbor);
                    openSet.Enqueue(neighbor, fScore[neighbor]);
                }
            }
        }

        return null;
    }
}
