using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchVisualizer : MonoBehaviour
{
    [SerializeField] private GameObject startPrefab;
    [SerializeField] private GameObject endPrefab;

    new private Camera _camera;

    private Vector2 _curPos;
    
    private Vector2 _startPos;
    private Vector2 _endPos;

    private void Start()
    {
        _camera = FindFirstObjectByType<Camera>();
    }
    
    public void OnPointerPressed(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            _startPos = _curPos;

            Vector2 pos = _camera.ScreenToWorldPoint(_startPos);
            
            GameObject startIndicator = Instantiate(startPrefab, pos, Quaternion.identity);
        }
        
        if (ctx.canceled)
        {
            _endPos = _curPos;
            Vector2 pos = _camera.ScreenToWorldPoint(_endPos);
            GameObject endIndicator = Instantiate(endPrefab, pos, Quaternion.identity);
        }
    }

    public void OnPointerPosition(InputAction.CallbackContext ctx)
    {
        _curPos = ctx.action.ReadValue<Vector2>();
    }

    private IEnumerator RemoveIndicators(GameObject start, GameObject end)
    {
        yield return new WaitForSeconds(1);
        Destroy(start);
        Destroy(end);
    }
}
