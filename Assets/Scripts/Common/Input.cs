using System;
using UnityEngine;
using UnityEngine.InputSystem;

public delegate void VectorDelegate(Vector2 vector);

public class Input : MonoBehaviour
{
    public event Action OnContact;
    public event Action OnRelease;
    public event VectorDelegate OnFlickPos;
    public event VectorDelegate OnFlickRelease;


    private PlayerInput _playerInput;
    private bool _touching;
    private Vector2 _touchStartPos;
    private Vector2 _touchEndPos;
    private Vector3 initialposition;

    private float _maxFlickDistance;
    private bool _lockInput;


    private void Awake()
    {
        _playerInput = new PlayerInput();
        _maxFlickDistance = (Screen.width < Screen.height ? Screen.width : Screen.height) * 0.5f;
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
        if (_lockInput) { return; }
        if (_touching)
        {
            _touchEndPos = _playerInput.Touch.PrimaryPosition.ReadValue<Vector2>();

            OnFlickPos?.Invoke(GetNormalizedFlickDelta());
        }
    }

    public void LockInput()
    {
        _lockInput = true;
    }
    public void UnlockInput()
    {
        _lockInput = false;
    }
    //Max flick distance is half of screen lesser dimension;
    private Vector2 GetNormalizedFlickDelta()
    {
        Vector2 delta = _touchEndPos - _touchStartPos;

        if (delta.y > 0) { return Vector2.zero; }

        delta.y = delta.y > 0 ? 0 : delta.y;

        float magnitude = delta.magnitude > _maxFlickDistance ? _maxFlickDistance : delta.magnitude;
        float normalizedMagnitude = magnitude / _maxFlickDistance;

        return -delta.normalized * normalizedMagnitude;
    }
    private void StartTouchPrimary(InputAction.CallbackContext ctx)
    {
        if (_lockInput) { return; }
        Vector2 point = _playerInput.Touch.PrimaryPosition.ReadValue<Vector2>();
        _touchStartPos = point;
        _touching = true;

        OnContact?.Invoke();
    }
    private void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        if (_lockInput) { return; }
        if (!_touching) { return; }
        transform.localPosition = initialposition;
        _touching = false;

        OnFlickRelease?.Invoke(GetNormalizedFlickDelta());
        OnRelease?.Invoke();
    }
}
