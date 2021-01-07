using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Profiling;

public class DijkstrasAlgorithm : MonoBehaviour
{
    public static event Action<PathNode, PathNode> ResetNodes;
    
    private List<PathNode> allNodes;
    private PathNode _currentNode;

    private void Start()
    {
        allNodes = GlobalFunctions.AllObjects();
    }
    
    public PathNode GetPathStep(PathNode start, PathNode target)
    {
        ResetNodes?.Invoke(start, target);
        _currentNode = start;
        
        while (allNodes.Find(n => !n.Explored))
        {
            ModifyDistanceValues(_currentNode.NeighborNodes);

            _currentNode = ClosestUnexplored();
            _currentNode.Explored = true;

            if (_currentNode == target) 
                break;
        }
        
        return Path(target).Reverse().ToList().First();
    }

    private void ModifyDistanceValues(List<PathNode> neighbors)
    {

        var unexploredNeighbors = from node in allNodes
            where !node.Explored && neighbors.Contains(node)
            select node;
        
        foreach (var uNeighbor in unexploredNeighbors)
        {
            float distanceFromNeighbor =
                Vector3.Distance(_currentNode.transform.position, uNeighbor.transform.position);
            float distanceFromStart = distanceFromNeighbor + _currentNode.Weight;

            if (distanceFromStart < uNeighbor.Weight)
            {
                uNeighbor.Weight = distanceFromStart;
                uNeighbor.NodePointingToMe = _currentNode;
            }
        }
    }

    private PathNode ClosestUnexplored()
    {
        //Create a list of unexplored nodes, excluding the current node
        var nodesToSearch = allNodes.Where(n => !n.Explored);
        return nodesToSearch.OrderBy(n => n.Weight).First();
    }

    private IEnumerable<PathNode> Path(PathNode target)
    {
        PathNode node = target;

        while (node.NodePointingToMe)
        {
            yield return node;
            node = node.NodePointingToMe;
        }
    }
}