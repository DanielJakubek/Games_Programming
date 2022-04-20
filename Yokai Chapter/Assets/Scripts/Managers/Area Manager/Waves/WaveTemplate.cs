using System.Collections.Generic;
using UnityEngine;

/* 
    Holds information on enemy waves. Basically,
    what enemies will be in that wave
*/
[CreateAssetMenu(fileName = "New Wave")]
public class WaveTemplate : ScriptableObject
{
    [Header("Wave Properties")]
    public List<EnemyWaveList> enemyList;
}

/* 
    Wave porperties
*/
[System.Serializable]
public struct EnemyWaveList{
    public string enemyName;
    public int amountOfEnemies;
}

