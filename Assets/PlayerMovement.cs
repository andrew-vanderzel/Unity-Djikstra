using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public DijkstrasAlgorithm algorithm;
    public Vector3 target;
    private float yOffset;
    void Start()
    {
        yOffset = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (algorithm.generatedPath.Count > 1)
        {
            var nodePosition = algorithm.generatedPath[1].position;
            target = new Vector3(nodePosition.x, yOffset, nodePosition.z);
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        
    }
}
