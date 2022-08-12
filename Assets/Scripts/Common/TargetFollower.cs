using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] Vector3 _offset;


    public void Follow(Transform newTarget)
    {
        _target = newTarget;
    }
    public void Unfollow()
    {
        _target = null;
    }

    private void FixedUpdate()
    {
        if (_target == null) { return; }

        transform.position = _target.position + _offset;
    }
}
