using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarInput : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Camera _camera;

    private bool _touching;
    private Vector2 _touchStartPos;
    private Vector2 _touchEndPos;

    private Vector3 initialposition;


    private void Awake()
    {
        _playerInput = new PlayerInput();
        _camera = Camera.main; 

    }
    private void OnEnable()
    {
        _playerInput.Enable();
    }
    private void OnDisable()
    {
        _playerInput.Disable();
    }
    private void Start()
    {
        _playerInput.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        _playerInput.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);

        initialposition = transform.localPosition;
    }
    private void Update()
    {
        if (_touching)
        {
            _touchEndPos = _playerInput.Touch.PrimaryPosition.ReadValue<Vector2>();

            Vector2 delta = _touchEndPos - _touchStartPos;

            Vector3 deltaXZ = new Vector3(delta.x, 0, delta.y);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, initialposition + deltaXZ.normalized, 0.1f);

            Debug.Log(deltaXZ);
        }
    }

    private void StartTouchPrimary(InputAction.CallbackContext ctx)
    {
        Debug.Log("start track");
        _touchStartPos = _playerInput.Touch.PrimaryPosition.ReadValue<Vector2>();
        _touching = true;
    }
    private void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        Debug.Log("end track");
        _touching = false;
        transform.localPosition = initialposition;
    }
}
