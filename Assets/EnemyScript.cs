using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public static event Action EnemyDeathActions;

    private void OnDestroy()
    {
        EnemyDeathActions?.Invoke();
        
       
    }
}
