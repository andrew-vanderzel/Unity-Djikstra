using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    public bool intersection { get; private set; }
    public List<PathNode> NeighborNodes { get; private set; }
    public PathNode NodePointingToMe {get; set;}
    public float Weight { get; set;}
    public bool Explored { get; set; }
    
    private List<PathNode> _allNodes;
    
    private void OnEnable() => DijkstrasAlgorithm.ResetNodes += ResetNode;
    private void OnDisable() => DijkstrasAlgorithm.ResetNodes -= ResetNode;

    private void Start()
    {
        _allNodes = FindObjectsOfType<PathNode>().ToList().FindAll(n => n.intersection == false);
    }

    //Reset values for when a new path is generated
    private void ResetNode(PathNode start, PathNode end)
    {
        Weight = (this == start) ? 0 : 1000;

        Explored = false;
        NodePointingToMe = null;

        GetNeighbors();
    }

    //Get neighbors within 6 units and are not blocked
    private void GetNeighbors()
    {
        NeighborNodes = (from node in _allNodes
            where WithinDistance(this, node, 6)
            where node != this
            where !node.intersection
            select node).ToList();
    }
    
    //Calculate distance between nodes
    private bool WithinDistance(PathNode from, PathNode to, float threshold)
    {
        return Vector3.Distance(from.transform.position, to.transform.position) < threshold;
    }
    
    //Detect if node is intersecting with an obsticle
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Obsticle")
            intersection = true;
    }
    //Detect if node stops intersecting with an obsticle
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Obsticle")
            intersection = false;
    }
}