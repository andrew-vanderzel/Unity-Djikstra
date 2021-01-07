using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public DijkstrasAlgorithm algorithm;
    
    public PathNode endPoint;
    public float speed;
    
    private Vector3 _target;
    private PathNode pathNodeScript;
    
    void Start()
    {
        pathNodeScript = transform.GetComponent<PathNode>();
    }

    // Update is called once per frame
    void Update()
    {
        //Set the next node to traverse to by using the algorithm
        if(Input.GetKeyDown(KeyCode.Space))
            _target = algorithm.GetPathStep(pathNodeScript, endPoint).transform.position;
        
        //Move towards the current destination
        if (_target != Vector3.zero)
            transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);
    }
}
