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
        _allNodes = GlobalFunctions.AllObjects();
    }

    private void ResetNode(PathNode start, PathNode end)
    {
        Weight = (this == start) ? 0 : 1000;

        Explored = false;
        NodePointingToMe = null;

        GetNeighbors();
    }

    private void GetNeighbors()
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
        if (other.tag == "Obsticle")
            intersection = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Obsticle")
            intersection = false;
    }
}