using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector3 P1 { get; }
    public Vector3 P2 { get; }
    public Vector3 P3 { get; }
    public Vector3 Center { get; }
    public float Area { get; }
    public List<Node> Neighbors { get; }

    public Node(Vector3 p1, Vector3 p2, Vector3 p3)
    {

        float d1 = Vector3.Distance(p1, p2);
        float d2 = Vector3.Distance(p2, p3);
        float d3 = Vector3.Distance(p3, p1);

        float cx = ((p1.x * d2) + (p2.x * d3) + (p3.x * d1)) / (d1 + d2 + d3);
        float cy = ((p1.y * d2) + (p2.y * d3) + (p3.y * d1)) / (d1 + d2 + d3);
        float cz = (Mathf.Max(p1.z, p2.z, p3.z) + Mathf.Min(p1.z, p2.z, p3.z)) / 2;

        this.Center = new Vector3(cx, cy, cz);
        this.P1 = p1;
        this.P2 = p2;
        this.P3 = p3;

        this.Area = (P1.x * (P2.z - P3.z) + P2.x * (P3.z - P1.z) + P3.x * (P1.z - P2.z)) / 2;

        this.Neighbors = new List<Node>();

    }

    public bool IsNeighbor(Node n)
    {
        return (P1 == n.P1 || P1 == n.P2 || P1 == n.P3 ? 1 : 0) + (P2 == n.P1 || P2 == n.P2 || P2 == n.P3 ? 1 : 0) + (P3 == n.P1 || P3 == n.P2 || P3 == n.P3 ? 1 : 0) == 2;
    }

    public void Draw(float duration)
    {
        Debug.DrawLine(P1, P2, Color.white, duration);
        Debug.DrawLine(P2, P3, Color.white, duration);
        Debug.DrawLine(P3, P1, Color.white, duration);
    }

    private float Sign (Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return (p1.x - p3.x) * (p2.z - p3.z) - (p2.x - p3.x) * (p1.z - p3.z);
    }

    public bool PointInTriangle (Vector3 pt)
    {
        float d1, d2, d3;
        bool has_neg, has_pos;

        d1 = Sign(pt, P1, P2);
        d2 = Sign(pt, P2, P3);
        d3 = Sign(pt, P3, P1);

        has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
        has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

        return !(has_neg && has_pos);
    }
}