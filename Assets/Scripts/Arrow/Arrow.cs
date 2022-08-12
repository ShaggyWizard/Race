using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Arrow : MonoBehaviour
{
    [SerializeField] private Input _input;
    [Header("Options")]
    [SerializeField, Range(1,100)] private float _lengthModifyer;
    [Header("Visual options")]
    [SerializeField] private Vector2 _foldPos;
    [SerializeField] private Vector2 _pointPos;
    
    
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    private Vector2 _target;

    private static readonly int[] triangles =
    {
        4,3,5,
        0,5,3,
        0,3,1,
        2,1,3
    };

    private void Awake()
    {
        if (_meshFilter == null) { _meshFilter = GetComponent<MeshFilter>(); }
        if (_meshRenderer == null) { _meshRenderer = GetComponent<MeshRenderer>(); }
    }

    private void OnEnable()
    {
        _input.OnFlickPos += SetTarget;
        _input.OnContact += EnableRenderer;
        _input.OnRelease += DisableRenderer;
    }
    private void OnDisable()
    {
        _input.OnFlickPos -= SetTarget;
        _input.OnContact -= EnableRenderer;
        _input.OnRelease -= DisableRenderer;
    }

    private void OnValidate()
    {
        Limit(ref _foldPos);
        Limit(ref _pointPos);
    }


    private void EnableRenderer()
    {
        _meshRenderer.enabled = true;
    }
    private void DisableRenderer()
    {
        _meshRenderer.enabled = false;
    }
    private void SetTarget(Vector2 vector)
    {
        _target = vector * _lengthModifyer;
        _meshFilter.mesh = CreateArrowMesh();
    }
    private void Limit(ref Vector2 vector)
    {
        if (vector.x < 0) vector.x = 0;
        if (vector.y < 0) vector.y = 0;
        if (vector.x > 1) vector.x = 1;
        if (vector.y > 1) vector.y = 1;
    }
    private Mesh CreateArrowMesh()
    {
        Vector3 offset = new Vector3(_target.x, 0, _target.y);
        var rotation = Quaternion.FromToRotation(Vector3.forward, offset);

        float magnitude = offset.magnitude;

        Vector3[] vertices = new Vector3[]
        {
            Vector3.zero,
            rotation * new Vector3(_foldPos.x, 0, _foldPos.y) * magnitude,
            rotation * new Vector3(_pointPos.x, 0, _pointPos.y) * magnitude,
            new Vector3(_target.x, 0, _target.y),
            rotation * new Vector3(-_pointPos.x, 0, _pointPos.y) * magnitude,
            rotation * new Vector3(-_foldPos.x, 0, _foldPos.y) * magnitude

        };

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        return mesh;
    }
}
