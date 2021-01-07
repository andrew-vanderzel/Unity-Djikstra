using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDraw : MonoBehaviour
{
    private FollowBehavior fBehavior;
    private LineRenderer lRenderer;
    void Start()
    {
        fBehavior = transform.parent.GetComponent<FollowBehavior>();
        lRenderer = GetComponent<LineRenderer>();
        
        
    }

    private void Update()
    {
        ReDraw();
    }

    public void ReDraw()
    {
        lRenderer.positionCount = fBehavior.path.Count;
  
        for (int i = 0; i < lRenderer.positionCount; i++)
        {
            Vector3 position = fBehavior.path[i].transform.position;

            position.y = transform.position.y;
            position.x = Mathf.Round(position.x / 5) * 5;
            position.z = Mathf.Round(position.z / 5) * 5;
            lRenderer.SetPosition(i, position);
        }
    }
}
