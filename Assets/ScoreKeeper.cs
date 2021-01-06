using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    public int enemiesKilled;

    private void OnEnable()
    {
        EnemyScript.EnemyDeathActions += IncreaseKillCount;
    }

    private void OnDisable()
    {
        EnemyScript.EnemyDeathActions -= IncreaseKillCount;
    }

    public void IncreaseKillCount()
    {
        enemiesKilled++;
    }
}
