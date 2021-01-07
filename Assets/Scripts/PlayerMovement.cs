using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PathNode endPoint;
    public float speed;
    public DijkstrasAlgorithm algorithm;
    public Vector3 target;
    private float yOffset;
    private PathNode pathNodeScript;
    void Start()
    {
        pathNodeScript = GetComponent<PathNode>();
        yOffset = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            target = algorithm.GetPathStep(pathNodeScript, endPoint).transform.position;

        if (target != Vector3.zero)
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
