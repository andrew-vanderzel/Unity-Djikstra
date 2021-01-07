using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetVisualFollow : MonoBehaviour
{
    public float speed;
    public Transform target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
    }
}
