using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FallChecker : MonoBehaviour
{
    [SerializeField] float _fallHeight;
    [SerializeField] private UnityEvent _onFall;


    private bool triggered;


    private void Update()
    {
        if (!triggered && transform.position.y < _fallHeight)
        {
            _onFall.Invoke();
            triggered = true;
        }
    }


    public void Reset()
    {
        triggered = false;
    }
}
