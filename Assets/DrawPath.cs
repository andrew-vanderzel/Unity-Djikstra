using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrawPath : MonoBehaviour
{
    public DijkstrasAlgorithm algorithm;
    public Gradient weightGradient;
    public float upperThreshold;

    public enum DrawViews
    {
        Normal,Weight
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach (var node in algorithm.generatedPath)
        {
            if (node != algorithm.generatedPath.Last())
            {
                var nList = algorithm.generatedPath;
                Debug.DrawLine(node.transform.position, 
                    nList[nList.IndexOf(node) + 1].transform.position, Color.white);
            }
        }
        
        
        foreach (var node in algorithm.AllNodes)
        {
            float weight = Mathf.InverseLerp(0, upperThreshold, node.Weight);
            Color weightColor = weightGradient.Evaluate(weight);
            if (node.transform.childCount > 0)
            {
                if (!node.intersection)
                {
                    node.transform.GetChild(0).GetComponent<Renderer>().material.color = weightColor;
                }
                else
                {
                    node.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.white;
                }
            }
                
        }
        
    }
}
