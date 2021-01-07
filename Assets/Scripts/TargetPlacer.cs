using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlacer : MonoBehaviour
{
    public static event Action ChangeEntityPaths;
    
    public Transform targetObject;
    public float height;
    private Vector3 _cursorPoint;
    private Vector3 _previousCursorPoint;
    
    void Start()
    {
        _cursorPoint = Vector3.zero;
        _cursorPoint = targetObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit) && (Input.GetMouseButton(0)))
        {
            if(hit.collider.tag != "Obsticle")
                _cursorPoint = hit.point;
        }

        _cursorPoint.x = Mathf.Round(_cursorPoint.x / 5) * 5;
        _cursorPoint.z = Mathf.Round(_cursorPoint.z / 5) * 5;

        _cursorPoint.y = height;


        targetObject.position = _cursorPoint;

        if (_cursorPoint != _previousCursorPoint)
        {
            _previousCursorPoint = _cursorPoint;
            ChangeEntityPaths?.Invoke();
        }
    }

}
