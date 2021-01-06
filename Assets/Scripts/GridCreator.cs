using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    public GameObject node;
    public int gridSize;
    public float spacing;
    [ContextMenu("Create Grid")]
    public void CreateGrid()
    {
   
        for (int i = 0;  i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                Instantiate(node, new Vector3(i * spacing, 0, j * spacing), Quaternion.identity);
            }
        }
    }
 
}
