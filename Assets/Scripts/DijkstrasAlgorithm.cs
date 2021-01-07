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
    
    private List<PathNode> _allNodes;
    private PathNode _currentNode;

    private void Start()
    {
        _allNodes = FindObjectsOfType<PathNode>().ToList();
    }

    //Get the next node to traverse to
    public PathNode GetPathStep(PathNode start, PathNode target)
    {
        //Reset previously set node values
        ResetNodes?.Invoke(start, target);
        _currentNode = start;

        //Create path while there are unexplored nodes and the target has not been reached
        while (_allNodes.Find(n => !n.Explored))
        {
            ModifyDistanceValues(_currentNode.NeighborNodes);

            //Set current node to the next optimal node and set the new node to explored
            _currentNode = ClosestUnexplored();
            _currentNode.Explored = true;
            
            //Finish searching if reached target
            if (_currentNode == target) 
                break;
        }
        
        return Path(target).Reverse().ToList().First();
    }

    //Set the weights of the current node's neighbors
    private void ModifyDistanceValues(List<PathNode> neighbors)
    {
        //Find unexplored neighbors
        var unexploredNeighbors = neighbors.Where(node => !node.Explored);
        
        foreach (var uNeighbor in unexploredNeighbors)
        {
            //Calculate current node distance from neighbor
            float distFromNeighbor = Vector3.Distance(_currentNode.transform.position, uNeighbor.transform.position);
            //Calculate the neighbor's distance from the start
            float distanceFromStart = distFromNeighbor + _currentNode.Weight;
            
            if (distanceFromStart < uNeighbor.Weight)
            {
                uNeighbor.Weight = distanceFromStart;
                uNeighbor.NodePointingToMe = _currentNode;
            }
        }
    }

    //Return the currently exploring node's cloest unexplored neighbor
    private PathNode ClosestUnexplored()
    {
        //Create a list of unexplored nodes, excluding the current node
        var nodesToSearch = _allNodes.Where(n => !n.Explored);
        return nodesToSearch.OrderBy(n => n.Weight).First();
    }

    //Create the path by traversing backwards from the end to the start
    private IEnumerable<PathNode> Path(PathNode target)
    {
        PathNode node = target;

        //Create the backwards path until the starting node has been reached
        while (node.NodePointingToMe)
        {
            yield return node;
            node = node.NodePointingToMe;
        }
    }
}