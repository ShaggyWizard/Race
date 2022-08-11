using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class RoadCreator : MonoBehaviour
{
    [SerializeField] private float _width;
    [SerializeField] private Vector2[] _angleAndRange;


    private Road _road;
    private MeshFilter _meshFilter;
    private MeshCollider _meshCollider;

    private const float halfRotation = 180f;
    private const float maxAngle = 179.9f;
    void OnValidate()
    {
        Vector2[] points = new Vector2[_angleAndRange.Length + 1];
        
        for (int i = 0; i < _angleAndRange.Length; i++)
        {
            if (i != 0)
            {
                if (_angleAndRange[i].x < _angleAndRange[i - 1].x - halfRotation) { _angleAndRange[i].x = _angleAndRange[i - 1].x - maxAngle; }
                if (_angleAndRange[i].x > _angleAndRange[i - 1].x + halfRotation) { _angleAndRange[i].x = _angleAndRange[i - 1].x + maxAngle; }
            }
            if (_angleAndRange[i].y < _width * 0.5f) { _angleAndRange[i].y = _width * 0.5f; }

            Quaternion rotation = Quaternion.Euler(0, _angleAndRange[i].x, 0);

            Vector3 vector = rotation * Vector3.forward * _angleAndRange[i].y;
            points[i + 1] = points[i] + new Vector2(vector.x, vector.z);
        }

        _road = new Road(points);

        if (_meshFilter == null) { _meshFilter = GetComponent<MeshFilter>(); }
        if (_meshCollider == null) { _meshCollider = GetComponent<MeshCollider>(); }

        Vector2[] vertices = Road.CalculateVertices(_road.Points.ToArray(), _width);
        _meshFilter.mesh = CreateRoadMesh(vertices);
        _meshCollider.sharedMesh = _meshFilter.sharedMesh;
    }

    private Mesh CreateRoadMesh(Vector2[] points)
    {
        if (points == null)
        {
            return null;
        }
        Vector3[] vertices = new Vector3[points.Length * 2];
        int[] triangles = new int[2 * (points.Length - 1) * 3];
        int t = 0;

        for (int i = 0; i < points.Length - 3; i += 2, t += 6)
        {
            vertices[i] = new Vector3(points[i].x, 0, points[i].y);
            vertices[i + 1] = new Vector3(points[i + 1].x, 0, points[i + 1].y);
            vertices[i + 2] = new Vector3(points[i + 2].x, 0, points[i + 2].y);
            vertices[i + 3] = new Vector3(points[i + 3].x, 0, points[i + 3].y);

            triangles[t] = i;
            triangles[t + 1] = i + 2;
            triangles[t + 2] = i + 1;

            triangles[t + 3] = i + 1;
            triangles[t + 4] = i + 2;
            triangles[t + 5] = i + 3;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        return mesh;
    }
}
