using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FollowBehavior : MonoBehaviour
{
    public DijkstrasAlgorithm algorithm;
    public PathNode endPoint;
    public float speed;
    public List<PathNode> path;
    private Vector3 _target;
    private PathNode pathNodeScript;

    private void OnEnable()
    {
        TargetPlacer.ChangeEntityPaths += RecalculatePath;
    }

    private void OnDestroy()
    {
        TargetPlacer.ChangeEntityPaths -= RecalculatePath;
    }

    void Start()
    {
        pathNodeScript = transform.GetComponent<PathNode>();
        InvokeRepeating("RecalculatePath", 0, 1);
        InvokeRepeating("SetNextPosition", 0, 1);
        
    }

    // Update is called once per frame
    void Update()
    {

        //Move towards the current destination
        if (_target != Vector3.zero)
            transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);
    }

    public void RecalculatePath()
    {
        path = path = algorithm.GetPath(pathNodeScript, endPoint);
    }
    public void SetNextPosition()
    {
        if(path.Count > 0)
            _target = path[0].transform.position;
    }
    
   
}
