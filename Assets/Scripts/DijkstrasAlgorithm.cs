using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Profiling;

public class DijkstrasAlgorithm : MonoBehaviour
{
    public PathNode startNode;
    public PathNode endNode;
    public List<Transform> generatedPath;
    public List<PathNode> AllNodes { get; private set; }
  
    private PathNode _currentNode;
    
    private void Start()
    {
        _currentNode = startNode;
        AllNodes = GlobalFunctions.AllObjects();
    }

    private void Update()
    {
        
        CreateNewPath();
    }


    [ContextMenu("Generate New Path")]
    public void CreateNewPath()
    {
       
        foreach (var node in AllNodes)
        {
            if (node != startNode)
                node.Weight = 100000;
            else
                node.Weight = 0;

            node.Explored = false;
            node.NodeToMe = null;
            startNode.GetNeighbors();
            endNode.GetNeighbors();
        }

        _currentNode = startNode;
        
        GeneratePath();
    }
    
    private void GeneratePath()
    {
        while (NodesToExplore())
        {
            ModifyDistanceValues(_currentNode, _currentNode.NeighborNodes);
            _currentNode = ClosestUnexplored(_currentNode);
            _currentNode.Explored = true;

            if (_currentNode == endNode) break;
        }
        
        generatedPath = Path();
    }
    
    private List<Transform> Path()
    {
        var pathToReturn = new List<Transform>();
        var cPathNode = endNode;
   
        while (cPathNode.NodeToMe)
        {
            pathToReturn.Add(cPathNode.transform);
            cPathNode = cPathNode.NodeToMe;
        }

        pathToReturn.Reverse();
        return pathToReturn;
    }
    
    private void ModifyDistanceValues(PathNode me, List<PathNode> neighbors)
    {
        var unexploredNeighbors = from node in AllNodes
            where !node.Explored 
            where neighbors.Contains(node)
            select node;
        
        foreach (var node in unexploredNeighbors)
        {
            float nodeDistanceFromMe = DistanceValue(me, node);
            float reachWeight = nodeDistanceFromMe + me.Weight;
            bool closer = reachWeight < node.Weight;
            
            if (closer)
            {
                node.Weight = reachWeight;
                node.NodeToMe = me;
            }
        }
    }
    
    private PathNode ClosestUnexplored(PathNode me)
    {
        var nodesToSearch = from node in AllNodes
            where node.Explored == false && node != me
            select node;

        return nodesToSearch.OrderBy(n => n.Weight).First();
    }
    
    private bool NodesToExplore()
    {
        return AllNodes.Find(node => !node.Explored);
    }
    
    private float DistanceValue(PathNode from, PathNode to)
    {
        return Vector3.Distance(from.transform.position, to.transform.position);
    }





 
}
