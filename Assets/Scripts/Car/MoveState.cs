using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Rigidbody))]
public class MoveState : MonoBehaviour
{
    [SerializeField] private UnityEvent _onStartMoving;
    [SerializeField] private UnityEvent _onStopMoving;


    private Rigidbody _rigidbody;
    private bool _moving;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (!_moving && _rigidbody.velocity.magnitude != 0)
        {
            Debug.Log("start");
            _onStartMoving.Invoke();
            _moving = true;
        }
        else if (_moving && _rigidbody.velocity.magnitude == 0)
        {
            Debug.Log("finish");
            _onStopMoving.Invoke();
            _moving = false;
        }
    }
}
