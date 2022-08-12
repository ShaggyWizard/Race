using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarMover : MonoBehaviour
{
    [SerializeField] private Input _input;
    [SerializeField] private float _modifyer;


    private Rigidbody _rigidbody;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        _input.OnFlickRelease += ApplyImpulse;
    }
    private void OnDisable()
    {
        _input.OnFlickRelease -= ApplyImpulse;
    }


    public void ResetVelocity()
    {
        _rigidbody.velocity = Vector3.zero;
    }


    private void ApplyImpulse(Vector2 force)
    {
        _rigidbody.AddForce(new Vector3(force.x, 0, force.y) * _modifyer, ForceMode.Impulse);
    }
}
