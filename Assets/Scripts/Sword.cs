using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject cutPrefab;
    private GameObject _swordPoint;
    
    private Camera _camera;
    private bool _isTouching;
    private Vector2 _curPos;
    private Vector2 _endPos;

    public void Start()
    {
        _camera = FindFirstObjectByType<Camera>();
    }

    public void OnPointerPressed(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            _isTouching = true;
            
            Vector2 pos = _camera.ScreenToWorldPoint(_curPos);
            
            _swordPoint = Instantiate(cutPrefab, pos, Quaternion.identity);
        }
        
        if (ctx.canceled)
        {
            _endPos = _curPos;
            _isTouching = false;
            
            Destroy(_swordPoint.gameObject);
        }
    }

    public void OnPointerPosition(InputAction.CallbackContext ctx)
    {
        _curPos = ctx.action.ReadValue<Vector2>();
        MoveSwordPoint(_curPos);
    }

    private void MoveSwordPoint(Vector2 pos)
    {
        if (_isTouching)
        {
            Vector3 newPos = new Vector3(pos.x, pos.y, 0);
            _swordPoint.transform.position = (Vector2)_camera.ScreenToWorldPoint(newPos);   
        }
    }
}
