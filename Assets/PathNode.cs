﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    public bool Explored {get; set;}
    public PathNode NodeToMe;
    public float Weight;
    public List<PathNode> NeighborNodes;
    private List<PathNode> _allNodes;
    public bool intersection;
    private void Start()
    {
        _allNodes = GlobalFunctions.AllObjects();
        GetNeighbors();
    }

    private void Update()
    {
        GetNeighbors();
    }


    public void GetNeighbors()
    {
        var nearbyNodes = from node in _allNodes
            where WithinDistance(this, node, 3) && node != this && !node.intersection
            select node;

        NeighborNodes = nearbyNodes.ToList();
    }
    
    private bool WithinDistance(PathNode from, PathNode to, float threshold)
    {
        return Vector3.Distance(from.transform.position, to.transform.position) < threshold;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Obsticle")
            intersection = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Obsticle")
            intersection = false;
    }
}