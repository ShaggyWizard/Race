using System.Collections.Generic;
using UnityEngine;

public class Road
{
    public List<Vector2> Points { get; private set; }
    public float _width { get; private set; }


    public Road(Vector2[] points)
    {
        Points = new List<Vector2>();
        Points.AddRange(points);
    }

    /// <summary>
    /// Calculates edges based on given points and width
    /// </summary>
    /// <param name="points">Ordered points</param>
    /// <param name="width">Desired width of road</param>
    /// <returns></returns>
    public static Vector2[] CalculateVertices(Vector2[] points, float width)
    {
        if (points.Length < 2) { return null; }

        float halfWidth = width * 0.5f;
        Vector2[] edges = new Vector2[points.Length * 2];

        int pointIndex = 0;
        int vertexIndex = 0;

        //First two edges based only on next point
        Vector2 direction = points[1] - points[0];
        Vector2 left = new Vector2(-direction.y, direction.x).normalized * halfWidth;
        edges[vertexIndex] = points[pointIndex] + left;
        edges[vertexIndex + 1] = points[pointIndex] - left;
        pointIndex++;
        vertexIndex += 2;


        //Calculate all not first and not last vertices
        for (; pointIndex < points.Length - 1; pointIndex++, vertexIndex += 2)
        {
            Vector2 lastVector = points[pointIndex - 1] - points[pointIndex];
            Vector2 nextVector = points[pointIndex + 1] - points[pointIndex];

            Vector2 outDirection = (lastVector.normalized + nextVector.normalized).normalized;
            if (outDirection.magnitude == 0)
            {
                outDirection = new Vector2(-nextVector.y, nextVector.x).normalized;
            }

            left = new Vector2(-nextVector.y, nextVector.x).normalized;
            left = Vector2.Dot(left, outDirection) >= 0 ? outDirection : -outDirection;

            float cos = Vector2.Dot(outDirection, lastVector.normalized);
            float sin = Mathf.Sqrt(1 - (cos * cos));
            float hypotenuse = halfWidth / sin;
            left *= hypotenuse;

            edges[vertexIndex] = points[pointIndex] + left;
            edges[vertexIndex + 1] = points[pointIndex] - left;
        }

        //Last point based only on previous point
        direction = points[points.Length - 1] - points[points.Length - 2];
        left = new Vector2(-direction.y, direction.x).normalized * halfWidth;

        edges[edges.Length - 2] = points[pointIndex] + left;
        edges[edges.Length - 1] = points[pointIndex] - left;

        return edges;
    }
}
