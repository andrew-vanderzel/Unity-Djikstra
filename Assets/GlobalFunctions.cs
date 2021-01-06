using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GlobalFunctions : MonoBehaviour
{
    public static List<PathNode> AllObjects()
    {
        return FindObjectsOfType<PathNode>().ToList().FindAll(n => n.intersection == false);
    }

}
