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
    
    private List<PathNode> _allUnblockedNodes;
    
    private void OnEnable() => DijkstrasAlgorithm.ResetNodes += ResetNode;
    private void OnDisable() => DijkstrasAlgorithm.ResetNodes -= ResetNode;

    //Reset values for when a new path is generated
    private void ResetNode(PathNode start, PathNode end)
    {
        _allUnblockedNodes = FindObjectsOfType<PathNode>().Where(node => !node.intersection && node != this).ToList();
        
        Weight = (this == start) ? 0 : 1000;
        Explored = false;
        NodePointingToMe = null;

        SetNeighbors();
    }

    //Get neighbors within 6 units and are not blocked
    private void SetNeighbors()
    {
        NeighborNodes = _allUnblockedNodes.Where(node => WithinDistance(this, node, 6)).ToList();
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