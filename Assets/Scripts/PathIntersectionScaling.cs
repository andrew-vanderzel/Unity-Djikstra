using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathIntersectionScaling : MonoBehaviour
{
    public float intersectScale;
    public float normalScale;
    public float currentScale;
    public float scaleSpeed;
    public AnimationCurve bounceCurve;
    private PathNode pNode;
    void Start()
    {
        pNode = GetComponent<PathNode>();
        normalScale = transform.GetChild(0).localScale.x;
        intersectScale = normalScale * intersectScale;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (pNode.intersection)
        {
            SetScale(intersectScale);
        }
        else
        {
            SetScale(normalScale);
        }
    }

    private void SetScale(float target)
    {
        Vector3 targetScale = new Vector3(target, target, target);
        var child = transform.GetChild(0);
        child.localScale = Vector3.Lerp(child.localScale, targetScale, scaleSpeed);
    }
}
